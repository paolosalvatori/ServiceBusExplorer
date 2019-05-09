namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class ReleaseInfo
    {
        public static ReleaseInfo Null => new ReleaseInfo(null, new Version(), string.Empty, null);
        public Uri ReleaseUri { get; private set; }
        public Version Version { get; private set; }
        public string Body { get; private set; }
        public Uri ZipPackageUri { get; private set; }

        public ReleaseInfo(Uri releaseUri, Version version, string body, Uri zipPackageUri)
        {
            ReleaseUri = releaseUri;
            Version = version;
            Body = body;
            ZipPackageUri = zipPackageUri;
        }
    }

    static class GitHubReleaseProvider
    {
        public class Release
        {
            [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
            public Uri Url { get; set; }

            [JsonProperty("html_url", NullValueHandling = NullValueHandling.Ignore)]
            public Uri HtmlUrl { get; set; }

            [JsonProperty("tag_name", NullValueHandling = NullValueHandling.Ignore)]
            public string TagName { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("draft", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Draft { get; set; }

            [JsonProperty("prerelease", NullValueHandling = NullValueHandling.Ignore)]
            public bool? Prerelease { get; set; }

            [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
            public List<Asset> Assets { get; set; }

            [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
            public string Body { get; set; }
        }

        public class Asset
        {
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
            public string Label { get; set; }

            [JsonProperty("browser_download_url", NullValueHandling = NullValueHandling.Ignore)]
            public Uri BrowserDownloadUrl { get; set; }
        }

        public static async Task<ReleaseInfo> GetServiceBusClientLatestVersion(WriteToLogDelegate writeToLog = null)
        {
            var nextReleaseInfo = ReleaseInfo.Null;
            using (var client = new HttpClient())
            {
                var responseBody = string.Empty;
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ASBE");
                    responseBody = await client.GetStringAsync("https://api.github.com/repos/paolosalvatori/ServiceBusExplorer/releases/latest")
                        .ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    if (writeToLog != null)
                    {
                        writeToLog(e.Message);
                    }
                    else
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (!string.IsNullOrWhiteSpace(responseBody))
                    try
                    {
                        var latestReleaseInfo = JsonConvert.DeserializeObject<Release>(responseBody);
                        if (latestReleaseInfo != null && !string.IsNullOrWhiteSpace(latestReleaseInfo.Name))
                        {
                            var version = new Version(latestReleaseInfo.Name);
                            var uri = latestReleaseInfo.HtmlUrl;
                            var zipUrl = latestReleaseInfo.Assets.FirstOrDefault(x => x.Name.EndsWith(".zip"))?.BrowserDownloadUrl;
                            nextReleaseInfo = new ReleaseInfo(uri, version,
                                latestReleaseInfo.Body, zipUrl);
                        }
                    }
                    catch (Exception e)
                    {
                        if (writeToLog != null)
                        {
                            writeToLog(e.Message);
                        }
                        else
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
            }

            return nextReleaseInfo;
        }
    }
}
