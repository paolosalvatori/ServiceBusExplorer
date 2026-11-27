using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    [CollectionDefinition("Checkpoint file", DisableParallelization = true)]
    public class CheckpointFileCollection { }

    [Collection("Checkpoint file")]
    public class EventHubCheckpointPersistenceTests : IDisposable
    {
        private readonly string directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        private readonly Dictionary<string, object> saved = new Dictionary<string, object>();
        private readonly Dictionary<string, EventProcessorCheckpointInfo> savedMap;
        private string FilePath => Path.Combine(directory, "checkpoints.json");
        private static FieldInfo Field(string name) => typeof(EventProcessorCheckpointHelper)
            .GetField(name, BindingFlags.NonPublic | BindingFlags.Static);
        private static Dictionary<string, EventProcessorCheckpointInfo> Map =>
            (Dictionary<string, EventProcessorCheckpointInfo>)Field("mapDictionary").GetValue(null);

        public EventHubCheckpointPersistenceTests()
        {
            Directory.CreateDirectory(directory);
            foreach (var name in new[] { "filePath", "itemList", "canWriteCheckpoints", "pendingSanitizedWrite" })
                saved[name] = Field(name).GetValue(null);
            savedMap = new Dictionary<string, EventProcessorCheckpointInfo>(Map);
            Map.Clear();
            Field("filePath").SetValue(null, FilePath);
            Field("itemList").SetValue(null, null);
            Field("canWriteCheckpoints").SetValue(null, false);
            Field("pendingSanitizedWrite").SetValue(null, false);
        }

        [Theory]
        [InlineData("Endpoint=sb://synthetic.servicebus.windows.net/", "synthetic.servicebus.windows.net")]
        [InlineData("HostName=synthetic.azure-devices.net", "legacy-iot:synthetic.azure-devices.net")]
        public void ReadAndSaveMergeEveryPartitionWithStableHighestSequence(string prefix, string identity)
        {
            var first = Item(prefix + ";SharedAccessKeyName=one;SharedAccessKey=synthetic-one",
                ("0", 5, "50"), ("1", 7, "70"), ("2", 9, "b"));
            var second = Item(prefix + ";SharedAccessKeyName=two;SharedAccessKey=synthetic-two",
                ("0", 8, "80"), ("3", 4, "40"), ("2", 9, "a"));
            string previous = null;
            foreach (var records in new[] { new[] { first, second }, new[] { second, first } })
            {
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(records));
                EventProcessorCheckpointHelper.ReadCheckpoints();
                var leases = EventProcessorCheckpointHelper.GetLease(identity, "hub", "$Default");
                Assert.Equal(4, leases.Count);
                Assert.Equal(8, leases["0"].SequenceNumber);
                Assert.Equal("70", leases["1"].Offset);
                Assert.Equal("40", leases["3"].Offset);
                Assert.Equal("a", leases["2"].Offset);
                var snapshot = JsonConvert.SerializeObject(leases.OrderBy(pair => pair.Key));
                if (previous != null) Assert.Equal(previous, snapshot);
                previous = snapshot;
                EventProcessorCheckpointHelper.WriteCheckpoints();
                var text = File.ReadAllText(FilePath);
                Assert.DoesNotContain("SharedAccessKey", text);
                Assert.DoesNotContain("synthetic-one", text);
                Assert.Single(JsonConvert.DeserializeObject<List<EventProcessorCheckpointInfo>>(text));
                EventProcessorCheckpointHelper.ReadCheckpoints();
                Assert.Equal(4, EventProcessorCheckpointHelper.GetLease(identity, "hub", "$Default").Count);
            }
        }

        [Fact]
        public void RemovedIdentitiesPersistAsEmptyArrayAndClearOldMap()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(new[] { Item("plain", ("0", 1, "1")) }));
            EventProcessorCheckpointHelper.ReadCheckpoints();
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(new[] { Item("SharedAccessKey=synthetic", ("0", 2, "2")) }));
            EventProcessorCheckpointHelper.ReadCheckpoints();
            Assert.Null(EventProcessorCheckpointHelper.GetLease("plain", "hub", "$Default"));
            EventProcessorCheckpointHelper.WriteCheckpoints();
            Assert.Equal("[]", File.ReadAllText(FilePath));
        }

        [Fact]
        public void FailedReadDoesNotOverwriteFileEvenAfterEarlierSuccessfulRead()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(new[] { Item("plain", ("0", 1, "1")) }));
            EventProcessorCheckpointHelper.ReadCheckpoints();
            File.WriteAllText(FilePath, "invalid synthetic json");
            Assert.ThrowsAny<JsonException>(() => EventProcessorCheckpointHelper.ReadCheckpoints());
            EventProcessorCheckpointHelper.WriteCheckpoints();
            Assert.Equal("invalid synthetic json", File.ReadAllText(FilePath));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async System.Threading.Tasks.Task FileDisabledCheckpointingRetainsLeasesWithoutPersisting(bool existingFile)
        {
            if (existingFile) File.WriteAllText(FilePath, "untouched synthetic file");
            var lease = new Lease { PartitionId = "0", SequenceNumber = 4, Offset = "4" };
            EventProcessorCheckpointHelper.SetLease("synthetic", "hub", "$Default", "0", lease);
            await EventProcessorCheckpointHelper.CheckpointAsync("synthetic", "hub", "$Default", lease, "5");
            Assert.Equal("5", EventProcessorCheckpointHelper.GetLease("synthetic", "hub", "$Default", "0").Offset);
            EventProcessorCheckpointHelper.WriteCheckpoints();
            if (existingFile) Assert.Equal("untouched synthetic file", File.ReadAllText(FilePath));
            else Assert.False(File.Exists(FilePath));
            Assert.False((bool)Field("canWriteCheckpoints").GetValue(null));
            EventProcessorCheckpointHelper.SetLease("synthetic", "hub", "$Default", "0", null);
            Assert.Null(EventProcessorCheckpointHelper.GetLease("synthetic", "hub", "$Default", "0").Offset);
        }

        [Fact]
        public void NeverLoadedFileIsNotOverwritten()
        {
            File.WriteAllText(FilePath, "untouched synthetic file");
            EventProcessorCheckpointHelper.WriteCheckpoints();
            Assert.Equal("untouched synthetic file", File.ReadAllText(FilePath));
        }

        [Fact]
        public void MissingFileAllowsNewCheckpointsWithoutCreatingAnEmptyFile()
        {
            EventProcessorCheckpointHelper.ReadCheckpoints();
            EventProcessorCheckpointHelper.WriteCheckpoints();
            Assert.False(File.Exists(FilePath));
            EventProcessorCheckpointHelper.SetLease("plain", "hub", "$Default", "0", new Lease { PartitionId = "0", SequenceNumber = 3, Offset = "3" });
            EventProcessorCheckpointHelper.WriteCheckpoints();
            Assert.Single(JsonConvert.DeserializeObject<List<EventProcessorCheckpointInfo>>(File.ReadAllText(FilePath)));
        }

        private static EventProcessorCheckpointInfo Item(string identity, params (string partition, long sequence, string offset)[] leases) =>
            new EventProcessorCheckpointInfo
            {
                Namespace = identity, EventHub = "hub", ConsumerGroup = "$Default",
                Leases = leases.ToDictionary(x => x.partition, x => new Lease
                    { PartitionId = x.partition, SequenceNumber = x.sequence, Offset = x.offset })
            };

        public void Dispose()
        {
            foreach (var pair in saved) Field(pair.Key).SetValue(null, pair.Value);
            Map.Clear();
            foreach (var pair in savedMap) Map.Add(pair.Key, pair.Value);
            Directory.Delete(directory, true);
        }
    }
}
