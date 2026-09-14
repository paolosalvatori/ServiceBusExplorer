using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Core.Services
{
    /// <summary>
    /// Cross-platform implementation of <see cref="ISavedConnectionsService"/> that
    /// persists profiles as JSON in the user's application-data directory.
    ///
    /// Storage locations:
    ///   Windows  → %APPDATA%\ServiceBusExplorer\connections.json
    ///   macOS    → ~/Library/Application Support/ServiceBusExplorer/connections.json
    ///   Linux    → ~/.config/ServiceBusExplorer/connections.json
    ///
    /// Passwords / SAS keys are stored in plain text in the user's profile directory.
    /// Future iterations should replace this with OS keychain integration.
    /// </summary>
    public sealed class JsonSavedConnectionsService : ISavedConnectionsService
    {
        private readonly string _filePath;
        private static readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);

        public JsonSavedConnectionsService()
            : this(DefaultFilePath()) { }

        public JsonSavedConnectionsService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IReadOnlyList<ConnectionProfile>> GetAllAsync(CancellationToken ct = default)
        {
            if (!File.Exists(_filePath))
                return Array.Empty<ConnectionProfile>();

            await _fileLock.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                var json = await ReadAllTextAsync(_filePath, ct).ConfigureAwait(false);
                var list = JsonConvert.DeserializeObject<List<ConnectionProfile>>(json);
                return (list ?? new List<ConnectionProfile>())
                    .OrderBy(p => p.DisplayName, StringComparer.OrdinalIgnoreCase)
                    .ToList()
                    .AsReadOnly();
            }
            catch (JsonException)
            {
                // Corrupt file – return empty rather than crashing
                return Array.Empty<ConnectionProfile>();
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task SaveAsync(ConnectionProfile profile, CancellationToken ct = default)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            if (string.IsNullOrWhiteSpace(profile.Name))
                throw new ArgumentException("Profile must have a non-empty Name.", nameof(profile));

            await _fileLock.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                var list = await LoadListUnlocked(ct).ConfigureAwait(false);
                var idx  = list.FindIndex(p =>
                    string.Equals(p.Name, profile.Name, StringComparison.OrdinalIgnoreCase));
                if (idx >= 0)
                    list[idx] = profile;
                else
                    list.Add(profile);

                await WriteListUnlocked(list, ct).ConfigureAwait(false);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task DeleteAsync(string name, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            await _fileLock.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                var list = await LoadListUnlocked(ct).ConfigureAwait(false);
                var removed = list.RemoveAll(p =>
                    string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

                if (removed > 0)
                    await WriteListUnlocked(list, ct).ConfigureAwait(false);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        // ── Internals ──────────────────────────────────────────────────────────────────────

        private async Task<List<ConnectionProfile>> LoadListUnlocked(CancellationToken ct)
        {
            if (!File.Exists(_filePath)) return new List<ConnectionProfile>();
            var json = await ReadAllTextAsync(_filePath, ct).ConfigureAwait(false);
            try   { return JsonConvert.DeserializeObject<List<ConnectionProfile>>(json) ?? new List<ConnectionProfile>(); }
            catch { return new List<ConnectionProfile>(); }
        }

        private async Task WriteListUnlocked(List<ConnectionProfile> list, CancellationToken ct)
        {
            var dir = Path.GetDirectoryName(_filePath)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            await WriteAllTextAsync(_filePath, json, ct).ConfigureAwait(false);
        }

        private static string DefaultFilePath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "ServiceBusExplorer", "connections.json");
        }

        // Polyfill for netstandard2.0 which lacks File.ReadAllTextAsync
        private static async Task<string> ReadAllTextAsync(string path, CancellationToken ct)
        {
#if NETSTANDARD2_0
            using var sr = new StreamReader(path);
            return await sr.ReadToEndAsync().ConfigureAwait(false);
#else
            return await File.ReadAllTextAsync(path, ct).ConfigureAwait(false);
#endif
        }

        private static async Task WriteAllTextAsync(string path, string content, CancellationToken ct)
        {
#if NETSTANDARD2_0
            using var sw = new StreamWriter(path, append: false);
            await sw.WriteAsync(content).ConfigureAwait(false);
#else
            await File.WriteAllTextAsync(path, content, ct).ConfigureAwait(false);
#endif
        }
    }
}

