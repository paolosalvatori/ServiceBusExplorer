using System;
using System.IO;
using System.Reflection;

using FluentAssertions;

using ServiceBusExplorer.Common.Helpers;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class MainSettingsDarkModeTests
    {
        static readonly MethodInfo GetMainSettingsUsingConfigurationMethod =
            typeof(ConfigurationHelper).GetMethod("GetMainSettingsUsingConfiguration",
                BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new InvalidOperationException("Unable to locate GetMainSettingsUsingConfiguration.");

        [Fact]
        public void SetDefault_DarkModeDefaultsToTrue()
        {
            var settings = new MainSettings();

            settings.SetDefault();

            settings.DarkMode.Should().BeTrue();
            settings.ThemeMode.Should().Be(ThemeMode.Dark);
        }

        [Fact]
        public void SetDefault_WhenDarkModeWasDisabled_ResetsDarkModeToTrue()
        {
            var settings = new MainSettings
            {
                DarkMode = false
            };

            settings.SetDefault();

            settings.DarkMode.Should().BeTrue();
        }

        [Fact]
        public void Equals_WhenDarkModeDiffers_ReturnsFalse()
        {
            var first = new MainSettings();
            first.SetDefault();

            var second = new MainSettings();
            second.SetDefault();
            second.DarkMode = !first.DarkMode;

            first.Equals(second).Should().BeFalse();
        }

        [Fact]
        public void Equals_WhenDarkModeMatches_ReturnsTrue()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.DarkMode = true;

            var second = new MainSettings();
            second.SetDefault();
            second.DarkMode = true;

            first.Equals(second).Should().BeTrue();
        }

        [Fact]
        public void LoadUsingConfiguration_WhenDarkModeIsPersisted_RoundTripsThroughConfiguration()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var expected = new MainSettings();
                expected.SetDefault();
                expected.DarkMode = true;

                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.DarkMode, expected.DarkMode);
                configuration.Save();

                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);
                loaded.DarkMode.Should().BeTrue();
                loaded.ThemeMode.Should().Be(ThemeMode.Dark);
                loaded.GetValue(ConfigurationParameters.DarkMode).Should().Be(expected.DarkMode);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void LoadUsingConfiguration_WhenDarkModeIsPersistedAsFalse_RoundTripsThroughConfiguration()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var expected = new MainSettings();
                expected.SetDefault();
                expected.DarkMode = false;

                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.DarkMode, expected.DarkMode);
                configuration.Save();

                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);
                loaded.DarkMode.Should().BeFalse();
                loaded.ThemeMode.Should().Be(ThemeMode.Light);
                loaded.GetValue(ConfigurationParameters.DarkMode).Should().Be(expected.DarkMode);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void LoadUsingConfiguration_WhenDarkModeIsMissing_PreservesCurrentSettingValue()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var currentSettings = new MainSettings();
                currentSettings.SetDefault();
                currentSettings.DarkMode = true;
                currentSettings.ShowMessageCount = false;

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);

                loaded.DarkMode.Should().BeTrue();
                loaded.ShowMessageCount.Should().BeFalse();
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void LoadUsingConfiguration_WhenThemeModeIsPersisted_OverridesLegacyDarkMode(ThemeMode mode)
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.DarkMode, mode != ThemeMode.Dark);
                configuration.SetValue(ConfigurationParameters.ThemeMode, mode);
                configuration.Save();

                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);
                loaded.ThemeMode.Should().Be(mode);
                loaded.GetValue(ConfigurationParameters.ThemeMode).Should().Be(mode);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void LoadUsingConfiguration_WhenThemeSettingsAreMissing_PreservesFollowOperatingSystem()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var currentSettings = new MainSettings();
                currentSettings.SetDefault();
                currentSettings.ThemeMode = ThemeMode.FollowOperatingSystem;

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);

                loaded.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void Equals_WhenLightAndFollowOperatingSystemDiffer_ReturnsFalse()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.ThemeMode = ThemeMode.Light;

            var second = new MainSettings();
            second.SetDefault();
            second.ThemeMode = ThemeMode.FollowOperatingSystem;

            first.DarkMode.Should().Be(second.DarkMode);
            first.Equals(second).Should().BeFalse();
        }

        [Fact]
        public void SetDefault_WhenFollowingOperatingSystem_ResetsThemeToDark()
        {
            var settings = new MainSettings { ThemeMode = ThemeMode.FollowOperatingSystem };

            settings.SetDefault();

            settings.ThemeMode.Should().Be(ThemeMode.Dark);
        }

        static MainSettings LoadMainSettings(string userConfigFilePath, ConfigFileUse configFileUse,
            MainSettings currentSettings)
        {
            var configuration = TwoFilesConfiguration.Create(userConfigFilePath, configFileUse);

            return (MainSettings)GetMainSettingsUsingConfigurationMethod.Invoke(null,
                new object[] { configuration, currentSettings, null });
        }

        static string CreateUserConfigFilePath()
        {
            var directory = Path.Combine(AppContext.BaseDirectory, "TestSettings");
            Directory.CreateDirectory(directory);

            return Path.Combine(directory, $"dark-mode-{Guid.NewGuid():N}.config");
        }

        static void DeleteUserConfigFilePath(string userConfigFilePath)
        {
            if (File.Exists(userConfigFilePath))
            {
                File.Delete(userConfigFilePath);
            }

            var directory = Path.GetDirectoryName(userConfigFilePath);
            if (!string.IsNullOrEmpty(directory) &&
                Directory.Exists(directory) &&
                Directory.GetFileSystemEntries(directory).Length == 0)
            {
                Directory.Delete(directory);
            }
        }
    }
}
