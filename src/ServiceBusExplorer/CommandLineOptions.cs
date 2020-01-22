namespace ServiceBusExplorer
{
    using CommandLine;
    using CommandLine.Text;
    using Helpers;

    public class CommandLineOptions
    {
        [Option('n', "namespace", SetName = "Connection", Required = false, HelpText = "Load with namespace.")]
        public string Namespace { get; set; }

        [Option('c', "connectionString", SetName = "Connection", Required = false, HelpText = "Load connection string.")]
        public string ConnectionString { get; set; }

        [Option('q', "queueFilter", Required = false, HelpText = "Set queue filter.")]
        public string QueueFilter { get; set; }

        [Option('t', "topicFilter", Required = false, HelpText = "Set topic filter.")]
        public string TopicFilter { get; set; }

        [Option('s', "subscriptionFilter", Required = false, HelpText = "Set subscription filter.")]
        public string SubscriptionFilter { get; set; }


        public static void ProcessCommandLineArguments(string[] args, out string argument, out string value, out string helpText)
        {
            argument = string.Empty;
            value = string.Empty;
            //CommandLineParser does not support slashes, to support legacy apps,
            //convert / to -. Can be removed if legacy apps are not a concern.
            for (var index = 0; index < args.Length; index++)
            {
                if (args[index].Length == 2 && args[index].StartsWith("/"))
                {
                    args[index] = args[index].Replace("/", "-");
                }
            }

            var localArgument = string.Empty;
            var localValue = string.Empty;
            var localHelpText = string.Empty;
            using (var parser = new Parser(config =>
            {
                config.HelpWriter = null;
                config.AutoVersion = false;
            }))
            {
                var parseResult = parser.ParseArguments<CommandLineOptions>(args);
                parseResult.WithParsed(o =>
                {
                    if (!string.IsNullOrWhiteSpace(o.Namespace))
                    {
                        localArgument = "/n";
                        localValue = o.Namespace.Trim();
                    }
                    else if (!string.IsNullOrWhiteSpace(o.ConnectionString))
                    {
                        localArgument = "/c";
                        localValue = o.ConnectionString.Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(o.QueueFilter))
                    {
                        FilterExpressionHelper.QueueFilterExpression = o.QueueFilter;
                    }

                    if (!string.IsNullOrWhiteSpace(o.TopicFilter))
                    {
                        FilterExpressionHelper.QueueFilterExpression = o.TopicFilter;
                    }

                    if (!string.IsNullOrWhiteSpace(o.SubscriptionFilter))
                    {
                        FilterExpressionHelper.QueueFilterExpression = o.SubscriptionFilter;
                    }
                })
                    .WithNotParsed(errors =>
                    {
                        localHelpText = HelpText.AutoBuild(parseResult, helpTextOptions =>
                        {
                            helpTextOptions.AddNewLineBetweenHelpSections = false;
                            helpTextOptions.AdditionalNewLineAfterOption = false;
                            helpTextOptions.Copyright = "";
                            helpTextOptions.Heading = "Command Line Options";
                            return HelpText.DefaultParsingErrorsHandler(parseResult, helpTextOptions);
                        });
                    });

                argument = localArgument;
                value = localValue;
                helpText = localHelpText;
            }
        }
    }
}
