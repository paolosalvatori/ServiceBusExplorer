using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class LegacyJwtCompatibilityTests
    {
        private static string SyntheticToken(string audience) => new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken("synthetic-issuer", audience, expires: DateTime.UtcNow.AddHours(1)));

        [Fact]
        public async Task LegacyServiceBusTokenProviderParsesSyntheticCallbackToken()
        {
            var calls = 0;
            var encoded = SyntheticToken("https://servicebus.azure.net/");
            var provider = Microsoft.ServiceBus.TokenProvider.CreateAzureActiveDirectoryTokenProvider(
                (audience, authority, state) => { calls++; return Task.FromResult(encoded); },
                new Uri("https://synthetic.invalid/tenant"), "https://servicebus.azure.net/", null);
            var token = await provider.GetTokenAsync("sb://synthetic.invalid/entity", "Send", true, TimeSpan.FromSeconds(5));
            Assert.Equal(1, calls);
            Assert.True(token.ValidTo > DateTime.UtcNow);
        }

        [Fact]
        public async Task RelayTokenProviderParsesSyntheticCallbackToken()
        {
            var calls = 0;
            var encoded = SyntheticToken("https://relay.azure.net/");
            var provider = Microsoft.Azure.Relay.TokenProvider.CreateAzureActiveDirectoryTokenProvider(
                (audience, authority, state) => { calls++; return Task.FromResult(encoded); },
                "https://synthetic.invalid/tenant", null);
            var token = await provider.GetTokenAsync("https://synthetic.invalid/relay", TimeSpan.FromMinutes(5));
            Assert.Equal(1, calls);
            Assert.Equal(encoded, token.TokenString);
            Assert.True(token.ExpiresAtUtc > DateTime.UtcNow);
        }

        [Fact]
        public void RuntimeLoadsMatchingPatchedIdentityModelFamily()
        {
            Assert.Equal(new Version(6, 36, 0, 0), typeof(JwtSecurityTokenHandler).Assembly.GetName().Version);
            Assert.Equal(new Version(6, 36, 0, 0), typeof(Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler).Assembly.GetName().Version);
            Assert.Equal(new Version(6, 36, 0, 0), typeof(Microsoft.IdentityModel.Tokens.TokenValidationParameters).Assembly.GetName().Version);
        }
    }
}
