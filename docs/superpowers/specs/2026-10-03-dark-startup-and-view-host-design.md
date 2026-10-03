# Dark Startup and Dynamic View Hosting Design

## Goal

Make dark mode the default for fresh configurations without overriding an existing user's explicit light-mode preference. Ensure dynamically loaded explorer content is fully themed and visible before it is drawn, eliminating the light content surface and blank queue view shown during initialization.

## Scope

The change covers startup theme resolution and the shared lifecycle used when explorer and container forms replace their hosted content. It does not change Service Bus operations, queue loading behavior, user preference persistence, or the availability of light mode.

## Theme resolution

1. Change the fresh-configuration default for `darkMode` to `true`.
2. Load and apply the selected configuration's stored theme during `MainForm` initialization before controls can be displayed.
3. Continue to honor an existing stored `darkMode=false` value.
4. Preserve the current high-contrast behavior through `ThemeManager.IsThemed` and `ThemeManager.Palette`; no callers will hard-code dark colors.

## Shared hosted-view lifecycle

Explorer views are currently replaced through repeated variants of this sequence: suspend drawing, dispose controls, clear the host, assign `SystemColors.GradientInactiveCaption`, create/add/size a new user control, and resume drawing. `ContainerForm` uses the same light-color reset pattern.

A small shared operation will own the common host lifecycle:

1. Suspend host drawing.
2. Dispose and remove the prior hosted user controls.
3. Assign the host background from the active theme palette when the application is themed, otherwise retain the appropriate light-system background.
4. Create, add, and size the new content control.
5. Apply the active theme synchronously to the content before drawing resumes.
6. Resume host and content drawing in a `finally` block.

`MainForm` will use this operation for all dynamically loaded entity views, including queues. `ContainerForm` will use its equivalent path for listener and test views. The current event subscriptions, locations, sizing, and existing `try`/`catch` boundaries remain at their present call sites.

## Failure behavior

The shared lifecycle helper will not suppress exceptions. Existing callers continue to route initialization failures through `HandleException`, retaining the application's logging and notification behavior instead of rendering an indistinguishable empty panel.

## Verification

1. Update settings tests so a fresh `MainSettings` defaults to dark and an explicit persisted light choice remains light.
2. Add a focused WinForms regression test for the hosted-view operation. With dark mode enabled, replacing content must leave the host on the dark palette and leave a child control visible and sized after drawing resumes.
3. Retain the existing dynamic-control theming coverage to verify controls introduced after root registration are themed.
4. Run the focused theme/settings test classes and build the affected project.

## Acceptance criteria

- A fresh configuration launches in dark mode.
- An existing configuration that explicitly chose light mode still launches light.
- A queue view and every other dynamically hosted explorer view receive the active theme before first visible rendering.
- Replacing a queue view does not leave its content controls blank, hidden, or partially rendered.
- High-contrast behavior and existing view-load error reporting remain unchanged.
