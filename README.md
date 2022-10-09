# <img src="assets/logo.svg" align="left" height="45"> heroicons-tag-helper

[![CI build status](https://github.com/xt0rted/heroicons-tag-helper/workflows/CI/badge.svg)](https://github.com/xt0rted/heroicons-tag-helper/actions?query=workflow%3ACI)
[![NuGet Package](https://img.shields.io/nuget/v/HeroiconsTagHelper?logo=nuget)](https://www.nuget.org/packages/HeroiconsTagHelper)
[![GitHub Package Registry](https://img.shields.io/badge/github-package_registry-yellow?logo=nuget)](https://nuget.pkg.github.com/xt0rted/index.json)
[![Project license](https://img.shields.io/github/license/xt0rted/heroicons-tag-helper)](LICENSE)

ASP.NET tag helper for adding [Heroicons](https://heroicons.com/) to your razor views.

## Installation

> ℹ️ This package's version is aligned with the [heroicons NPM package](https://www.npmjs.com/package/heroicons) so you always know which version you're using.

```terminal
dotnet add package HeroiconsTagHelper
```

CI builds can be downloaded from the [packages page](https://github.com/xt0rted/heroicons-tag-helper/packages/473445) or the GitHub feed.
They're also available as build artifacts on every PR.

## Setup

In your `_ViewImports.cshtml` add:

```html
@addTagHelper *, HeroiconsTagHelper
```

In your `Startup.cs` add:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHeroicons(Configuration);
}
```

In your `appsettings.json` add:

```json
{
  "Heroicons": {
    "IncludeComments": true,
    "SetAccessibilityAttributes": true,
    "SetFocusableAttribute": true
  }
}
```

## Settings

Name | Default Value | Description
:-- | :-- | :--
`IncludeComments` | `false` | Add an html comment before the svg tag with the style and name of the icon to help make development/debugging easier.
`SetAccessibilityAttributes` | `false` | Adds various accessibility attributes based on the default state of the tag.
`SetFocusableAttribute` | `false` | Adds the `focusable` attribute set to `false` to prevent the icon from receiving focus in Internet Explorer and Edge Legacy.

### SetAccessibilityAttributes

If `aria-label` or `aria-labeledby` are set then the icon is being used as an image so [`role="img"`](https://developer.mozilla.org/en-US/docs/Web/Accessibility/ARIA/Roles/Role_Img#svg_and_roleimg) will be added to the svg tag, otherwise [`aria-hidden="true"`](https://developer.mozilla.org/en-US/docs/Web/Accessibility/ARIA/ARIA_Techniques/Using_the_aria-hidden_attribute) will be added.

> **Note**: If the element already contains an `aria-hidden` or `role` attribute then it will not be modified.

## Usage

There are two versions of the tag helper which are used to pick between the `outline` and `solid` icon styles.

```html
<heroicon-outline icon="Sun" class="h-6 w-6 text-orange-500" />
```

will output

```xml
<svg class="h-6 w-6 text-orange-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z" />
</svg>
```

```html
<heroicon-solid icon="Sun" class="h-6 w-6 text-orange-500" />
```

will output

```xml
<svg class="h-6 w-6 text-orange-500" fill="currentColor" viewBox="0 0 20 20">
    <path fill-rule="evenodd" d="M10 2a1 1 0 011 1v1a1 1 0 11-2 0V3a1 1 0 011-1zm4 8a4 4 0 11-8 0 4 4 0 018 0zm-.464 4.95l.707.707a1 1 0 001.414-1.414l-.707-.707a1 1 0 00-1.414 1.414zm2.12-10.607a1 1 0 010 1.414l-.706.707a1 1 0 11-1.414-1.414l.707-.707a1 1 0 011.414 0zM17 11a1 1 0 100-2h-1a1 1 0 100 2h1zm-7 4a1 1 0 011 1v1a1 1 0 11-2 0v-1a1 1 0 011-1zM5.05 6.464A1 1 0 106.465 5.05l-.708-.707a1 1 0 00-1.414 1.414l.707.707zm1.414 8.486l-.707.707a1 1 0 01-1.414-1.414l.707-.707a1 1 0 011.414 1.414zM4 11a1 1 0 100-2H3a1 1 0 000 2h1z" clip-rule="evenodd" />
</svg>
```

The `outline` style also lets you set the `stroke-width` attribute which will be passed down to any paths that support it.
The Heroicons default is `2`, but this will let you adjust it as needed.

```html
<heroicon-outline icon="Sun" class="h-6 w-6 text-orange-500" stroke-width="1" />
```

will output

```xml
<svg class="h-6 w-6 text-orange-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1" d="M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z" />
</svg>
```

## Development

This project uses the [run-script](https://github.com/xt0rted/dotnet-run-script) dotnet tool to manage its build and test scripts.
To use this you'll need to run `dotnet tool install` and then `dotnet r` to see the available commands or look at the `scripts` section in the [global.json](global.json).
