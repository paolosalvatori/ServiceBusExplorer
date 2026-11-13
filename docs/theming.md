# Developing themed UI

Service Bus Explorer supports a dark theme and Windows high-contrast. The theme is applied at runtime by
`ThemeManager` (`src/ServiceBusExplorer/UIHelpers/Theming`). Most controls are themed automatically, so new UI
usually needs little extra work. This guide explains what to do, and what to avoid.

## How it works

- `ThemeManager.Register(root)` themes `root` and all of its children, and re-themes them when the user changes
  the theme mode (Options > Theme) or the OS theme changes. It also themes controls added later.
- Colors come from `ThemeManager.Palette` (`ThemePalette`): `Background`, `Surface`, `Raised`, `Border`, `Text`,
  `MutedText`, `Accent`, `Selection`, `Hover`, plus warning/error/success colors.
- Original designer values are captured and restored when switching back to the light theme, so do not rely on
  what the designer set being overwritten permanently.
- `ThemeManager.IsThemed` is `true` for dark and high-contrast, `ThemeManager.IsDark` only for dark.

## Adding a new form

Derive from `ThemedForm` instead of `Form`. It registers the form with `ThemeManager` on load and applies the
dark title bar. Forms derived from `ContainerForm` are already themed.

```csharp
public partial class MyForm : ThemedForm
{
    public MyForm() => InitializeComponent();
}
```

## Adding a new control or dialog content

- Controls placed in a registered form (including `UserControl`s) are themed automatically. Do nothing special.
- Controls shown outside a themed form (for example a context menu or a standalone `ToolStripDropDown`) need
  `ThemeManager.Register(control)`. Register on the control's UI thread.
- Standalone components that are not part of the control tree can be passed with
  `ThemeManager.RegisterComponents(components)`.
- Panels whose content is swapped at runtime should use `ThemeManager.SetHostedBackground(host)` and
  `ThemeManager.ReplaceHostedContent(...)` so new content is themed and old content is disposed.
- Header Azure wordmark logos and matching header status text can opt into theme-adapted branding with
  `ThemeManager.SetHeaderBrandLogo(pictureBox)` and `ThemeManager.SetHeaderBrandText(control)` (Azure blue in
  light mode, white in dark mode, and `WindowText` in high contrast).
- A control that needs custom behavior when the theme changes can implement `IThemeAware.ApplyTheme()`; it is
  called after the control and its children have been themed or restored.

## Do and don't

- **Don't hard-code colors** such as `Color.White` or `SystemColors.Window` for backgrounds and text. Prefer
  inherited/default colors so the theme can apply. When custom drawing is unavoidable, branch on
  `ThemeManager.IsThemed` and use `ThemeManager.Palette`.
- **Subscribe to theme changes in custom-drawn controls.** Implement `IThemeAware` and invalidate in
  `ApplyTheme()`.
- **Colors that carry meaning** (red/yellow/green/blue) are mapped to the palette's error, warning, success and
  accent colors, so keep using ordinary semantic colors in the designer.
- **Owner-drawn tabs:** the theme sizes fixed-size tabs (`ItemSize`) to fit their text. Don't rely on a tiny
  hard-coded `ItemSize` for long captions.
- **Data the user edits must not be recolored.** Color editing controls are excluded from theming on purpose.
- **Always test both themes**, and high contrast, by changing Options > Theme. Open every new dialog and check
  hover, disabled, selected and focus states.
