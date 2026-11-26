# Central dependencies and the legacy JWT compatibility bridge

Package versions are owned by `src/Directory.Packages.props`; projects retain
versionless `PackageReference` entries. Transitive pinning does not add a JWT
reference to projects that do not already consume it.

## JWT decision (#903)

`WindowsAzure.ServiceBus` 7.0.1 and `Microsoft.Azure.Relay` 3.0.1 both declare
`System.IdentityModel.Tokens.Jwt >= 6.7.1` in their official NuGet dependencies.
This change pins **6.36.0**, the latest 6.x patch, instead of jumping two major
versions to 8.2.1. The Microsoft advisory identifies **6.34.0** as patched for
CVE-2024-21319. Jwt 6.36.0 brings JsonWebTokens and Tokens >= 6.36.0; there is no
independent JsonWebTokens pin to drift away from it.

This is a temporary compatibility bridge, **not a supported long-term version**:
Microsoft has deprecated 6.x and recommends 8.x. Moving these legacy adapters to
a supported dependency family requires a separate compatibility assessment and
live authentication validation. No claim of complete live compatibility or freedom
from other vulnerabilities is made by this focused remediation.

The resolved graph uses Jwt, JsonWebTokens, Tokens and Logging 6.36.0 in the
existing legacy consumers. Abstractions remains 8.14.0 through Azure.Identity/MSAL;
it is not forced down, and modern-only projects do not gain Jwt. Synthetic tests
exercise both SDKs' actual AAD token-provider callback/parsing entry points and
check the loaded JWT assemblies. They do not contact Azure or test service-side
authentication, managed identity, Conditional Access, or the entire SDK surface.

Sources:
- [Microsoft advisory](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/security/advisories/GHSA-8g9c-28fc-mcx2)
- [Jwt 6.36.0 dependencies and deprecation](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/6.36.0)
- [Service Bus 7.0.1 dependencies](https://www.nuget.org/packages/WindowsAzure.ServiceBus/7.0.1)
- [Relay 3.0.1 dependencies](https://www.nuget.org/packages/Microsoft.Azure.Relay/3.0.1)

## Other deliberate changes

Centralizing Azure.Identity at **1.21.0** also aligns Relay's previous direct
**1.13.2** reference. This changes Relay's authentication dependency; offline
provider checks do not replace a live Relay sign-in/connection check.

`src/nuget.config` deliberately clears inherited feeds and maps every package to
nuget.org for deterministic source selection. Corporate mirrors and disconnected
restores must provide their own explicit configuration, for example:

```text
dotnet restore src/ServiceBusExplorer.slnx --configfile /path/to/company.nuget.config
```

The replacement configuration must list the approved mirror/offline feed and map
all required package IDs to it. The mirror must contain the selected versions and
their dependencies. Do not place credentials in repository files; use the approved
credential provider. A machine/user feed alone does not override the repository's
`clear` and wildcard source mapping.

## Integrating package changes from #908 and #929

Git's textual merge is insufficient: a versioned PackageReference fails CPM with
NU1008, while retaining an old central version may break the incoming source.
The following reconciliation is included now that both PRs are in the base branch:

| Incoming work | Central package version | Project reconciliation |
|---|---|---|
| #908 | Microsoft.Azure.NotificationHubs **4.2.0** | Remove `Version` from that reference in Common, NotificationHubs and ServiceBusExplorer projects. |
| #929 | Azure.Identity.Broker **1.7.0** | Add the central entry and remove `Version` from its Common reference. Preserve its explicit AnyCPU application target. |

Keep all incoming application changes; do not resolve a project conflict by
restoring the old SDK. Build the combined solution, inspect `project.assets.json`
for the versions above and the JWT family, and run exact-head Windows CI. Repeat
this check when merging additional package changes. Local builds do not replace
exact-head Windows or live validation.
