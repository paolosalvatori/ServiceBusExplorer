using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    // Both readers and writers use the process-wide test assembly .config file.
    [CollectionDefinition("Configuration file access", DisableParallelization = true)]
    public class ConfigurationFileCollection { }
}
