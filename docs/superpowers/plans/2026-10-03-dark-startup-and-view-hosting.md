# Dark Startup and View Hosting Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Start fresh installations in dark mode while preserving persisted light-mode choices, and synchronously theme every dynamically hosted view before it can render.

**Architecture:** Add a generic hosted-content replacement operation to the existing partial `ThemeManager`. It owns the current repeated drawing-suspension, disposal, background-reset, add/size, synchronous-theme, and resume sequence. `MainForm` and `ContainerForm` will delegate their view replacements to it, leaving entity construction, event subscription, sizing, and exception handling at the existing call sites.

**Tech Stack:** C# 7+/WinForms on .NET Framework 4.7.2, xUnit 2.9.3, FluentAssertions 6.12.0.

## Global Constraints

- A fresh configuration must default `darkMode` to `true`.
- An existing persisted `darkMode=false` must continue to launch in light mode.
- Use `ThemeManager.IsThemed` and `ThemeManager.Palette`; do not introduce hard-coded dark colors.
- High-contrast behavior must remain unchanged.
- Do not suppress or replace existing view-load exceptions and `HandleException` behavior.
- Do not alter Service Bus operations, preference serialization, user event subscriptions, control locations, or sizing logic.
- Keep all WinForms UI tests on an STA thread and in the existing `Theme UI` non-parallel collection.

---

## File Structure

| File | Responsibility |
|---|---|
| `src\ServiceBusExplorer\App.config` | Default application configuration for a new installation. |
| `src\Common\Helpers\MainSettings.cs` | In-memory default used when creating settings. |
| `src\ServiceBusExplorer\UIHelpers\Theming\ThemeManager.Hosting.cs` | New focused partial class containing the generic hosted-content lifecycle API. |
| `src\ServiceBusExplorer\Forms\MainForm.cs` | Routes every dynamic explorer entity view through the shared lifecycle API. |
| `src\ServiceBusExplorer\Forms\ContainerForm.cs` | Routes listener/test views through the shared lifecycle API. |
| `src\ServiceBusExplorer.Tests\Helpers\MainSettingsDarkModeTests.cs` | Verifies defaults and persisted dark/light preference behavior. |
| `src\ServiceBusExplorer.Tests\Forms\DarkModeThemeTests.cs` | Verifies replacement content is dark, visible, and sized before drawing resumes. |
| `src\ServiceBusExplorer.Tests\Forms\OptionFormDarkModeTests.cs` | Keeps reset-to-default and unsaved-selection coverage aligned with the new default. |

### Task 1: Make dark mode the fresh-settings default

**Files:**
- Modify: `src\ServiceBusExplorer\App.config`
- Modify: `src\Common\Helpers\MainSettings.cs`
- Modify: `src\ServiceBusExplorer.Tests\Helpers\MainSettingsDarkModeTests.cs`

**Interfaces:**
- Consumes: `ConfigurationParameters.DarkMode`, `MainSettings.SetDefault()`, and the existing private `ConfigurationHelper.GetMainSettingsUsingConfiguration` reflection helper.
- Produces: `MainSettings.DarkMode == true` for a newly initialized settings instance, while `TwoFilesConfiguration` continues to round-trip either Boolean value.

- [x] **Step 1: Write failing default and persisted-light tests**

Change the two default-setting assertions in `MainSettingsDarkModeTests` so they prove a clean settings object becomes dark:

```csharp
[Fact]
public void SetDefault_DarkModeDefaultsToTrue()
{
    var settings = new MainSettings();

    settings.SetDefault();

    settings.DarkMode.Should().BeTrue();
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
```

Add a separate persisted-light regression test; it must set `expected.DarkMode = false`, save it through `TwoFilesConfiguration`, load it with `LoadMainSettings`, and assert both `loaded.DarkMode` and `loaded.GetValue(ConfigurationParameters.DarkMode)` are `false`:

```csharp
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
        loaded.GetValue(ConfigurationParameters.DarkMode).Should().Be(expected.DarkMode);
    }
    finally
    {
        DeleteUserConfigFilePath(userConfigFilePath);
    }
}
```

- [x] **Step 2: Run the focused settings test to verify the new default tests fail**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter FullyQualifiedName~MainSettingsDarkModeTests
```

Expected: the two renamed default tests fail because `MainSettings.SetDefault()` currently assigns `false`.

- [x] **Step 3: Implement the two fresh-default changes**

In `MainSettings.SetDefault()`, replace:

```csharp
DarkMode = false;
```

with:

```csharp
DarkMode = true;
```

In `App.config`, replace the application setting:

```xml
<add key="darkMode" value="false" />
```

with:

```xml
<add key="darkMode" value="true" />
```

Do not modify `ConfigurationHelper`: its existing load behavior already preserves an explicit persisted `false`.

- [x] **Step 4: Run the focused settings tests to verify defaults and preferences**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter FullyQualifiedName~MainSettingsDarkModeTests
```

Expected: all `MainSettingsDarkModeTests` pass, including persisted `true`, persisted `false`, and missing-setting preservation.

- [x] **Step 5: Commit the settings default**

```powershell
git add src\ServiceBusExplorer\App.config src\Common\Helpers\MainSettings.cs src\ServiceBusExplorer.Tests\Helpers\MainSettingsDarkModeTests.cs
git commit -m "feat: default fresh installations to dark mode" -m "Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
```

### Task 2: Add a synchronous themed hosted-content lifecycle

**Files:**
- Create: `src\ServiceBusExplorer\UIHelpers\Theming\ThemeManager.Hosting.cs`
- Modify: `src\ServiceBusExplorer.Tests\Forms\DarkModeThemeTests.cs`

**Interfaces:**
- Consumes: `ControlHelper.SuspendDrawing()`, `ControlHelper.ResumeDrawing()`, `ThemeManager.IsThemed`, `ThemeManager.Palette`, and `ThemeManager.Apply(Control)`.
- Produces: `ThemeManager.ReplaceHostedContent<T>(Control host, Func<T> createContent, Action<T> configureContent)`, which returns the configured child after it has been synchronously themed and before drawing resumes.

- [x] **Step 1: Write the failing hosted-content regression test**

Add this test to `DarkModeThemeTests`, inside the existing `Theme UI` collection. It uses the existing `RunOnSta` helper and calls `ThemeManager.SetDarkMode(false)` in the `finally` block:

```csharp
[Fact]
public void ReplaceHostedContent_AppliesDarkThemeAndKeepsContentVisibleBeforeDrawingResumes()
{
    RunOnSta(() =>
    {
        using (var form = new ThemedForm { Size = new Size(400, 300) })
        using (var host = new Panel { Dock = DockStyle.Fill })
        {
            form.Controls.Add(host);
            form.Show();
            ThemeManager.SetDarkMode(true);

            var content = ThemeManager.ReplaceHostedContent(
                host,
                () => new Panel { Size = new Size(240, 160), Visible = true },
                control => control.Location = new Point(12, 18));

            host.BackColor.Should().Be(ThemeManager.Palette.Background);
            content.BackColor.Should().Be(ThemeManager.Palette.Background);
            content.Visible.Should().BeTrue();
            content.Bounds.Should().Be(new Rectangle(12, 18, 240, 160));
            host.Controls.Should().ContainSingle().Which.Should().BeSameAs(content);

            form.Close();
        }
    });
}
```

- [x] **Step 2: Run the focused UI test to verify it fails because the API does not exist**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter FullyQualifiedName~DarkModeThemeTests.ReplaceHostedContent_AppliesDarkThemeAndKeepsContentVisibleBeforeDrawingResumes
```

Expected: compilation fails because `ThemeManager.ReplaceHostedContent` is not defined.

- [x] **Step 3: Implement the hosted-content API in a focused partial class**

Create `ThemeManager.Hosting.cs` with the same namespace as the existing theme manager. Import `System`, `System.Drawing`, `System.Linq`, `System.Windows.Forms`, and `ServiceBusExplorer.UIHelpers`.

Implement this public generic API:

```csharp
public static T ReplaceHostedContent<T>(
    Control host,
    Func<T> createContent,
    Action<T> configureContent)
    where T : Control
```

Implement the method with the following behavior and order:

```csharp
if (host == null)
    throw new ArgumentNullException(nameof(host));
if (createContent == null)
    throw new ArgumentNullException(nameof(createContent));

host.SuspendDrawing();
T content = null;
try
{
    foreach (var child in host.Controls.OfType<UserControl>().ToArray())
        child.Dispose();
    host.Controls.Clear();
    host.BackColor = IsThemed ? Palette.Background : SystemColors.GradientInactiveCaption;

    content = createContent();
    if (content == null)
        throw new InvalidOperationException("The hosted content factory returned null.");

    content.SuspendDrawing();
    host.Controls.Add(content);
    configureContent?.Invoke(content);
    Apply(content);
    return content;
}
finally
{
    host.ResumeDrawing();
    if (content != null && !content.IsDisposed)
        content.ResumeDrawing();
}
```

The helper must intentionally dispose only `UserControl` instances, matching the existing `MainForm` behavior. It must not catch exceptions: content-construction and configuration errors need to continue to the existing caller-level `HandleException` paths.

- [x] **Step 4: Run the focused UI test to verify the host is synchronously themed**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter FullyQualifiedName~DarkModeThemeTests.ReplaceHostedContent_AppliesDarkThemeAndKeepsContentVisibleBeforeDrawingResumes
```

Expected: the new test passes and verifies the host palette, child palette, child visibility, child bounds, and child ownership.

- [x] **Step 5: Run all theme tests to protect existing dynamic-control behavior**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter FullyQualifiedName~DarkModeThemeTests
```

Expected: all `DarkModeThemeTests` pass.

- [x] **Step 6: Commit the lifecycle API and regression coverage**

```powershell
git add src\ServiceBusExplorer\UIHelpers\Theming\ThemeManager.Hosting.cs src\ServiceBusExplorer.Tests\Forms\DarkModeThemeTests.cs
git commit -m "fix: theme hosted controls before rendering" -m "Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
```

### Task 3: Route explorer and container views through the lifecycle API

**Files:**
- Modify: `src\ServiceBusExplorer\Forms\MainForm.cs:5437-6064`
- Modify: `src\ServiceBusExplorer\Forms\ContainerForm.cs:114-496`
- Test: `src\ServiceBusExplorer.Tests\Forms\DarkModeThemeTests.cs`
- Test: `src\ServiceBusExplorer.Tests\Forms\OptionFormDarkModeTests.cs`

**Interfaces:**
- Consumes: `ThemeManager.ReplaceHostedContent<T>(Control, Func<T>, Action<T>)`.
- Produces: all dynamic entity, listener, and test views attach through the same dispose/theme/resume sequence before they are made visible.

- [x] **Step 1: Write a call-site audit checklist in the PR description or implementation notes**

Before editing, enumerate every existing assignment to:

```csharp
panelMain.BackColor = SystemColors.GradientInactiveCaption;
```

The initial search finds 17 matches in `MainForm.cs` and 7 in `ContainerForm.cs`. One MainForm match belongs to a commented-out `TestRelay(RelayWrapper, bool)` implementation and must remain untouched. The active scope is 16 MainForm paths and 7 ContainerForm constructors (9 creation branches).

#### Call-site audit

All MainForm controls retain `Location = new Point(1, panelLog.HeaderHeight + 1)`. "Sized" means the existing `SetControlSize` call is retained. Event names below refer to the unchanged `MainForm_On...` handlers.

| MainForm path | Created control | Layout | Events |
|---|---|---|---|
| `ShowEventGridNamespace` | `HandleEventGridNamespaceControl` | Sized | None |
| `ShowQueue` | `HandleQueueControl` | Sized | Cancel, Refresh, ChangeStatus |
| `ShowTopic` | `HandleTopicControl` | Sized | Cancel, Refresh, ChangeStatus |
| `ShowEventGridTopic` | `HandleEventGridTopicControl` | Location only | None |
| `ShowSubscription` | `HandleSubscriptionControl` | Sized | Cancel, Refresh, ChangeStatus |
| `ShowEventGridSubscription` | `HandleEventGridSubscriptionControl` | Sized | None |
| `ShowRelay` | `HandleRelayControl` | Sized | Cancel, Refresh |
| `ShowRule` | `HandleRuleControl` | Sized | Cancel |
| `ShowEventHub` | `HandleEventHubControl` | Sized | Cancel, Refresh, ChangeStatus |
| `ShowPartition` | `HandlePartitionControl` | Sized; retain conditional partition fetch | Refresh |
| `ShowConsumerGroup` | `HandleConsumerGroupControl` | Sized | Cancel, Refresh |
| `ShowNotificationHub` | `HandleNotificationHubControl` | Sized | Cancel, Refresh |
| `TestQueue` (SDI) | `TestQueueControl` | Sized | Cancel |
| `TestTopic` (SDI) | `TestTopicControl` | Sized | Cancel |
| `TestSubscription` (SDI) | `TestSubscriptionControl` | Sized | Cancel |
| `TestRelay` (SDI) | `TestRelayControl` | Sized | Cancel |

All ContainerForm controls retain `Location = new Point(1, panelMain.HeaderHeight + 1)` and all four edge anchors. Standard size is `new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26)`. Each test control retains its original cancel-click unsubscription and `BtnCancelOnClick` subscription. Form titles, panel headers, focus calls, send-mode tab/button/group adjustments, trace listeners, and enclosing form layout suspension remain at the call sites.

| ContainerForm constructor/branch | Created control | Size/splitter | Events |
|---|---|---|---|
| Queue listener | `ListenerControl` | Width minus 3; session height 544/520; splitter 570 or height plus 26 | None |
| Queue send/test | `TestQueueControl` | Standard | Cancel click |
| Topic send/test | `TestTopicControl` | Standard | Cancel click |
| Subscription listener | `ListenerControl` | Width minus 3; session height 544/520; splitter 570 or height plus 26 | None |
| Subscription test | `TestSubscriptionControl` | Standard | Cancel click |
| Event hub/partition send | `TestEventHubControl` | Standard | Cancel click |
| Consumer group/partition listener | `PartitionListenerControl` | Standard | None |
| Connection-string/IoT listener | `PartitionListenerControl` | Standard | None |
| Relay test | `TestRelayControl` | Standard | Cancel click |

- [x] **Step 2: Replace each `MainForm` hosted-view sequence**

For every explorer method that currently does all of the following:

```csharp
panelMain.SuspendDrawing();
foreach (var userControl in panelMain.Controls.OfType<UserControl>())
    userControl.Dispose();
panelMain.Controls.Clear();
panelMain.BackColor = SystemColors.GradientInactiveCaption;
// construct a control
// call SuspendDrawing, Controls.Add, and SetControlSize
```

replace that lifecycle code with `ThemeManager.ReplaceHostedContent`. Keep construction arguments unchanged. Pass the existing layout work in `configureContent`; attach existing control events immediately after the helper returns. For `ShowQueue`, the resulting shape is:

```csharp
queueControl = ThemeManager.ReplaceHostedContent(
    panelMain,
    () => new HandleQueueControl(WriteToLog, serviceBusHelper, queue, path, duplicateQueue),
    control =>
    {
        control.Location = new Point(1, panelLog.HeaderHeight + 1);
        SetControlSize(control);
    });
queueControl.OnCancel += MainForm_OnCancel;
queueControl.OnRefresh += MainForm_OnRefresh;
queueControl.OnChangeStatus += MainForm_OnChangeStatus;
```

Apply the same form to event-grid namespace, topic, subscription, relay, rule, event hub, partition, consumer group, and notification-hub views. Preserve each control's existing header text, constructor arguments, locations, `SetControlSize` usage, and event handler wiring.

Remove the matching `SuspendDrawing`/`ResumeDrawing` and child `ResumeDrawing` blocks from these methods; the shared helper now provides the `try`/`finally` drawing guarantee. Do not remove the outer `try`/`catch (Exception ex) { HandleException(ex); }`.

- [x] **Step 3: Replace each `ContainerForm` hosted-view sequence**

In each `ContainerForm` constructor path that resets `panelMain.BackColor` to `SystemColors.GradientInactiveCaption`, replace the clear/reset/add/resume lifecycle with the same `ThemeManager.ReplaceHostedContent` call.

For listener and test controls, keep all currently calculated `Location`, `Size`, `Anchor`, form `Text`, splitter distances, and button/tab configuration in the configure action or immediately after the helper returns. The created `ListenerControl` and `TestQueueControl` must be returned by the helper so their existing members and event wiring remain available.

Retain existing `SuspendLayout`/`ResumeLayout` calls that manage the enclosing form. Remove only the duplicated `panelMain.SuspendDrawing`, `panelMain.ResumeDrawing`, and direct `SystemColors.GradientInactiveCaption` assignments now owned by `ThemeManager.ReplaceHostedContent`.

Implementation detail: queue/subscription listener layout and splitter changes remain inside the content factory, before attachment. Their existing `SplitterMoved` handler resizes hosted content and resumes drawing; moving the splitter after attachment would change the listener size and could resume drawing before synchronous theming. All other layout work uses the configure action.

- [x] **Step 4: Run focused tests after all call-site conversions**

Run:

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore --filter "FullyQualifiedName~MainSettingsDarkModeTests|FullyQualifiedName~DarkModeThemeTests"
```

Expected: all selected settings and theme tests pass.

- [x] **Step 5: Build the solution**

Run:

```powershell
dotnet build src\ServiceBusExplorer.sln --configuration Debug --no-restore
```

Expected: build succeeds with no errors.

- [ ] **Step 6: Manually verify the recorded queue scenario**

1. Start `ServiceBusExplorer` using a fresh configuration.
2. Confirm the main form and explorer content are dark before interaction.
3. Select a queue and wait for initialization to complete.
4. Confirm the action buttons and queue content remain visible and the panel never transitions to a blank white surface.
5. Open Options, disable dark mode, save, restart, and confirm the existing preference still produces the light UI.
6. Re-enable dark mode, restart, and confirm all dynamically loaded entity views use the dark palette.

- [x] **Step 7: Commit the converted view hosts**

```powershell
git add src\ServiceBusExplorer\Forms\MainForm.cs src\ServiceBusExplorer\Forms\ContainerForm.cs
git commit -m "fix: render dynamic views with active theme" -m "Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
```

## Final Verification

- [x] **Step 1: Run the full test project**

```powershell
dotnet test src\ServiceBusExplorer.Tests\ServiceBusExplorer.Tests.csproj --no-restore
```

Expected: all tests pass with zero failures.

- [x] **Step 2: Inspect the final diff for prohibited light reset paths**

Run:

```powershell
git diff --check
git diff -- src\ServiceBusExplorer\App.config src\Common\Helpers\MainSettings.cs src\ServiceBusExplorer\UIHelpers\Theming\ThemeManager.Hosting.cs src\ServiceBusExplorer\Forms\MainForm.cs src\ServiceBusExplorer\Forms\ContainerForm.cs src\ServiceBusExplorer.Tests\Helpers\MainSettingsDarkModeTests.cs src\ServiceBusExplorer.Tests\Forms\DarkModeThemeTests.cs
```

Expected: no whitespace errors; only the scoped default, hosted-content lifecycle, call-site, and test changes are present.

- [ ] **Step 3: Confirm the acceptance criteria**

Verify each item against the implementation and the fresh manual run:

1. New installations use dark mode.
2. Persisted `darkMode=false` remains light after restart.
3. Every explorer/container hosted view is synchronously themed before drawing resumes.
4. Queue content remains visible after initialization.
5. High-contrast palette selection and existing `HandleException` behavior remain unchanged.

## Implementation verification (2026-10-03)

- Settings red/green cycle: two new default assertions failed before implementation; all 7 settings tests passed afterward, including persisted dark/light values.
- Hosted-content red/green cycle: compilation failed for the missing API; the completed helper passed the theme suite. Additional STA coverage checks synchronous first-host-paint colors/bounds, disposal of multiple previous user controls, non-user-control detachment, light-mode colors (respecting the existing high-contrast selector), argument validation, and drawing recovery with unchanged factory/configuration exceptions.
- All 16 active MainForm paths and all 9 creation branches across the 7 ContainerForm constructors now use the helper. No active hosted-view path retains a direct light reset or `panelMain.Controls.Add`.
- The Options reset regression now expects dark defaults and starts from explicit light settings. The unsaved-selection test also starts explicitly light so it continues to exercise a checkbox change.
- An offline test initializes the actual `HandleQueueControl` using a synthetic entity-scoped localhost connection string (no Azure calls), then verifies six action buttons are visible, inside their parents, and themed both immediately and after queued UI events.
- Final `dotnet test ... --no-restore`: **167 passed, 0 failed, 0 skipped**. Final Debug solution build: **0 errors**, with the existing missing `MinimumRecommendedRules.ruleset` warning. `git diff --check` passed.
- **Manual acceptance remains pending:** the live Azure queue-selection and Options-save/restart scenario was not performed. No live Service Bus connection was supplied, and existing personal configurations were not modified. The automated results do not claim to replace that manual check.
