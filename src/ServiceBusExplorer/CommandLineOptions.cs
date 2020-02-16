namespace ServiceBusExplorer
{
    using System.Collections.Generic;
    using System.Windows.Forms.Design;
    using CommandLine;
    using CommandLine.Text;
    using Helpers;

    public class CommandLineOptions
    {
        [Option('n', "namespace", SetName = "Connection", Required = false, HelpText = "Load namespace key in the configuration file.")]
        public string Namespace { get; set; }

        [Option('c', "connectionString", SetName = "Connection", Required = false, HelpText = "Use a connection string.")]
        public string ConnectionString { get; set; }

        [Option('q', "queueFilter", Required = false, HelpText = "Set queue odata filter expression.")]
        public string QueueFilter { get; set; }

        [Option('t', "topicFilter", Required = false, HelpText = "Set topic odata filter expression.")]
        public string TopicFilter { get; set; }

        [Option('s', "subscriptionFilter", Required = false, HelpText = "Set subscription odata filter expression.")]
        public string SubscriptionFilter { get; set; }

        [Usage(ApplicationAlias = "ServiceBusExplorer")]
        public static IEnumerable<Example> Examples =>
            new List<Example>() {
                new Example("1. Use a namespace named paolosalvatori and set filters on queue, topic and subscription where they all start with \"request\"", 
                    new CommandLineOptions
                    {
                        Namespace = "paolosalvatori",
                        QueueFilter = "Startswith(Path, 'request') Eq true",
                        TopicFilter = "Startswith(Path, 'request') Eq true",
                        SubscriptionFilter = "Startswith(Path, 'request') Eq true"
                    }),
                new Example("2. Use a connection string set topic filters where starts with \"request\"",
                    new CommandLineOptions
                    {
                        ConnectionString = "Endpoint=sb://xxxx.servicebus.windows.net/;SharedAccessKeyName=keyName;SharedAccessKey=sharedAccessKey",
                        TopicFilter = "Startswith(Path, 'request') Eq true"
                    })
            };


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
                        FilterExpressionHelper.TopicFilterExpression = o.TopicFilter;
                    }

                    if (!string.IsNullOrWhiteSpace(o.SubscriptionFilter))
                    {
                        FilterExpressionHelper.SubscriptionFilterExpression = o.SubscriptionFilter;
                    }
                })
                    .WithNotParsed(errors =>
                    {
                        localHelpText = HelpText.AutoBuild(parseResult, helpTextOptions =>
                        {
                            helpTextOptions.AddNewLineBetweenHelpSections = true;
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
