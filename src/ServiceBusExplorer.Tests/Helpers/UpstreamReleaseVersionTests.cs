using System;
using System.Diagnostics;
using System.IO;
using FluentAssertions;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class UpstreamReleaseVersionTests : IDisposable
    {
        readonly string repositoryRoot = Path.Combine(Path.GetTempPath(), "Upstream release tests " + Guid.NewGuid().ToString("N"));
        readonly string initialCommit;

        public UpstreamReleaseVersionTests()
        {
            Directory.CreateDirectory(repositoryRoot);
            Git("init -q");
            Commit();
            initialCommit = Git("rev-parse HEAD").Trim();
        }

        [Fact]
        public void SelectsHighestStableUpstreamVersionAndIgnoresForkAndPrereleaseTags()
        {
            Git("update-ref refs/upstream-release-tags/6.9.0 HEAD");
            Git("update-ref refs/upstream-release-tags/v6.10.0 HEAD");
            Git("update-ref refs/upstream-release-tags/7.0.0-preview HEAD");
            Git("update-ref refs/upstream-release-tags/not-a-version HEAD");
            Git("tag 99.0.0");

            ResolveVersion().Should().Be("6.10.0");
        }

        [Fact]
        public void IncludesAnnotatedUpstreamTags()
        {
            Git("-c user.name=Test -c user.email=test@example.invalid tag --no-sign -a 6.3.1 -m release");
            Git("update-ref refs/upstream-release-tags/6.3.1 refs/tags/6.3.1");

            ResolveVersion().Should().Be("6.3.1");
        }

        [Fact]
        public void IgnoresNewerUpstreamReleasesNotInTheCurrentAncestry()
        {
            Git("update-ref refs/upstream-release-tags/6.3.1 HEAD");
            Commit();
            Git("update-ref refs/upstream-release-tags/7.0.0 HEAD");
            Git("checkout -q --detach " + initialCommit);

            ResolveVersion().Should().Be("6.3.1");
        }

        [Fact]
        public void UpdatesTheBaselineWhenANewerUpstreamReleaseIsIncorporated()
        {
            Git("update-ref refs/upstream-release-tags/6.3.1 HEAD");
            ResolveVersion().Should().Be("6.3.1");
            Commit();
            Git("update-ref refs/upstream-release-tags/7.0.0 HEAD");

            ResolveVersion().Should().Be("7.0.0");
        }

        [Fact]
        public void MissingUpstreamTagsReturnsUnknownInsteadOfTheForkVersion()
        {
            Git("tag 99.0.0");

            ResolveVersion().Should().BeEmpty();
        }

        [Fact]
        public void ShallowHistoryReturnsUnknown()
        {
            Commit();
            var shallowRoot = Path.Combine(repositoryRoot, "shallow clone");
            var repositoryUri = new Uri(repositoryRoot + Path.DirectorySeparatorChar).AbsoluteUri;
            Git($"clone -q --depth=1 \"{repositoryUri}\" \"{shallowRoot}\"");
            Run("git", "update-ref refs/upstream-release-tags/6.3.1 HEAD", shallowRoot);

            ResolveVersion(shallowRoot).Should().BeEmpty();
        }

        [Fact]
        public void SourceArchiveWithoutGitMetadataReturnsUnknown()
        {
            var archiveRoot = Path.Combine(repositoryRoot, "source archive");
            Directory.CreateDirectory(archiveRoot);

            ResolveVersion(archiveRoot).Should().BeEmpty();
        }

        public void Dispose()
        {
            foreach (var file in Directory.GetFiles(repositoryRoot, "*", SearchOption.AllDirectories))
            {
                File.SetAttributes(file, FileAttributes.Normal);
            }
            Directory.Delete(repositoryRoot, true);
        }

        void Commit()
        {
            Git("-c user.name=Test -c user.email=test@example.invalid commit --no-gpg-sign -q --allow-empty -m baseline");
        }

        string Git(string arguments)
        {
            return Run("git", arguments, repositoryRoot);
        }

        string ResolveVersion(string root = null)
        {
            var scriptPath = Path.Combine(AppContext.BaseDirectory, "GetUpstreamReleaseVersion.ps1");
            return Run("powershell.exe",
                $"-NoProfile -NonInteractive -ExecutionPolicy Bypass -File \"{scriptPath}\" -RepositoryRoot \"{root ?? repositoryRoot}\"",
                repositoryRoot).Trim();
        }

        static string Run(string executable, string arguments, string workingDirectory)
        {
            var startInfo = new ProcessStartInfo(executable, arguments)
            {
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = Process.Start(startInfo);
            var output = process.StandardOutput.ReadToEndAsync();
            var error = process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            process.ExitCode.Should().Be(0, error.GetAwaiter().GetResult());
            return output.GetAwaiter().GetResult();
        }
    }
}
