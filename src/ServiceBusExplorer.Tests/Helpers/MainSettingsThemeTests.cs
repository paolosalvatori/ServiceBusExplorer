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
    public class MainSettingsThemeTests
    {
        static readonly MethodInfo GetMainSettingsUsingConfigurationMethod =
            typeof(ConfigurationHelper).GetMethod("GetMainSettingsUsingConfiguration",
                BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new InvalidOperationException("Unable to locate GetMainSettingsUsingConfiguration.");

        [Fact]
        public void Constructor_DefaultsToFollowOperatingSystem()
        {
            var settings = new MainSettings();

            settings.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
        }

        [Fact]
        public void SetDefault_ThemeModeDefaultsToFollowOperatingSystem()
        {
            var settings = new MainSettings();

            settings.SetDefault();

            settings.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void SetDefault_WhenThemeWasChanged_ResetsToFollowOperatingSystem(ThemeMode mode)
        {
            var settings = new MainSettings
            {
                ThemeMode = mode
            };

            settings.SetDefault();

            settings.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
        }

        [Fact]
        public void Equals_WhenThemeModeDiffers_ReturnsFalse()
        {
            var first = new MainSettings();
            first.SetDefault();

            var second = new MainSettings();
            second.SetDefault();
            second.ThemeMode = ThemeMode.Light;

            first.Equals(second).Should().BeFalse();
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void Equals_WhenThemeModeMatches_ReturnsTrue(ThemeMode mode)
        {
            var first = new MainSettings();
            first.SetDefault();
            first.ThemeMode = mode;

            var second = new MainSettings();
            second.SetDefault();
            second.ThemeMode = mode;

            first.Equals(second).Should().BeTrue();
        }

        [Fact]
        public void LoadUsingConfiguration_WhenLegacyDarkModeIsTrue_SelectsDark()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.LegacyDarkMode, true);
                configuration.Save();

                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);
                loaded.ThemeMode.Should().Be(ThemeMode.Dark);
                loaded.GetValue(ConfigurationParameters.ThemeMode).Should().Be(ThemeMode.Dark);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void LoadUsingConfiguration_WhenLegacyDarkModeIsFalse_SelectsLight()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.LegacyDarkMode, false);
                configuration.Save();

                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);
                loaded.ThemeMode.Should().Be(ThemeMode.Light);
                loaded.GetValue(ConfigurationParameters.ThemeMode).Should().Be(ThemeMode.Light);
            }
            finally
            {
                DeleteUserConfigFilePath(userConfigFilePath);
            }
        }

        [Fact]
        public void LoadUsingConfiguration_WhenThemeSettingsAreMissing_PreservesCurrentSettingValue()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var currentSettings = new MainSettings();
                currentSettings.SetDefault();
                currentSettings.ThemeMode = ThemeMode.Dark;
                currentSettings.ShowMessageCount = false;

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig, currentSettings);

                loaded.ThemeMode.Should().Be(ThemeMode.Dark);
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
        public void LoadUsingConfiguration_WhenThemeModeIsPersisted_RoundTripsWithoutLegacySetting(ThemeMode mode)
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var configuration = TwoFilesConfiguration.Create(userConfigFilePath, ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.ThemeMode, mode);
                configuration.Save();

                var loaded = LoadMainSettings(userConfigFilePath, ConfigFileUse.UserConfig,
                    new MainSettings().GetDefault());

                loaded.ThemeMode.Should().Be(mode);
                loaded.GetValue(ConfigurationParameters.ThemeMode).Should().Be(mode);
                configuration.GetStringValue(ConfigurationParameters.LegacyDarkMode).Should().BeEmpty();
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
                configuration.SetValue(ConfigurationParameters.LegacyDarkMode, mode != ThemeMode.Dark);
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
        public void LoadUsingConfiguration_WhenThemeSettingsAreMissing_UsesSystemDefault()
        {
            var userConfigFilePath = CreateUserConfigFilePath();

            try
            {
                var currentSettings = new MainSettings();
                currentSettings.SetDefault();

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

            first.Equals(second).Should().BeFalse();
        }

        [Fact]
        public void GetDefault_UsesFollowOperatingSystem()
        {
            var settings = new MainSettings().GetDefault();

            settings.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
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

            return Path.Combine(directory, $"theme-mode-{Guid.NewGuid():N}.config");
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
