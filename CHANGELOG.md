# Changelog

## Unreleased

- Bumped `heroicons` from 2.0.15 to 2.0.16

## [2.0.15](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.14...v2.0.15) - 2023-02-14

- Bumped `heroicons` from 2.0.14 to 2.0.15

## [2.0.14](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.13...v2.0.14) - 2023-02-14

- Bumped `heroicons` from 2.0.13 to 2.0.14

## [2.0.13](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.12...v2.0.13) - 2022-11-13

- Bumped `heroicons` from 2.0.12 to 2.0.13

## [2.0.12](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.11...v2.0.12) - 2022-10-11

- Bumped `heroicons` from 2.0.11 to 2.0.12

## [2.0.11](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.10...v2.0.11) - 2022-10-11

- Bumped `heroicons` from 2.0.10 to 2.0.11
  - Adds new icons: `bug-ant`, `eye-dropper`, `pause-circle`, `play-circle`, `power`, `rocket-launch`, `square-2-stack-3d`, `stop-circle`, `trophy`, `tv`, `viewfinder-circle`, `window`

## [2.0.10](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.9...v2.0.10) - 2022-10-11

- Bumped `heroicons` from 2.0.9 to 2.0.10

## [2.0.9](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.8...v2.0.9) - 2022-10-11

- Bumped `heroicons` from 2.0.8 to 2.0.9
  - Adds new icons: `arrow-small-down`, `arrow-small-left`, `arrow-small-right`, `arrow-small-up`, `battery-0`, `battery-100`, `battery-50`, `cube-transparent`, `currency-bangladeshi`, `minus-small`, `paint-brush`, `plus-small`, `variable`, `wallet`, `arrow-path-rounded-square`

## [2.0.8](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.7...v2.0.8) - 2022-10-11

- Bumped `heroicons` from 2.0.7 to 2.0.8
  - Adds new icons: `user-minus`

## [2.0.7](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.6...v2.0.7) - 2022-10-11

- Bumped `heroicons` from 2.0.6 to 2.0.7

## [2.0.6](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.5...v2.0.6) - 2022-10-11

- Bumped `heroicons` from 2.0.5 to 2.0.6

## [2.0.5](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.4...v2.0.5) - 2022-10-11

- Bumped `heroicons` from 2.0.4 to 2.0.5

## [2.0.4](https://github.com/xt0rted/heroicons-tag-helper/compare/v2.0.3...v2.0.4) - 2022-10-11

- Bumped `heroicons` from 2.0.3 to 2.0.4

## [2.0.3](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.5...v2.0.3) - 2022-10-11

> **Note**: Due to a number of missing/incorrectly named icons the following versions were skipped: v2.0.0, v2.0.1, v2.0.2

- Bumped `heroicons` from 1.0.6 to 2.0.3 ([#186](https://github.com/xt0rted/heroicons-tag-helper/pull/186))
  - A lot of icons were renamed in this release, see the `Update icon names` section in the [2.0.0 release notes](https://github.com/tailwindlabs/heroicons/releases/tag/v2.0.0) for more details
  - The new mini variant is available using the tag name `heroicon-mini` which supports the same settings as the solid variant
- Dropped support for .NET 5 which is no longer supported
- Fixed xml documentation file so it's included in the package ([#189](https://github.com/xt0rted/heroicons-tag-helper/pull/189))
- Adjusted `IconAccessibilityTagHelper` so it only sets the `aria-hidden` or `role` attributes if they don't already exist ([#191](https://github.com/xt0rted/heroicons-tag-helper/pull/191))
  - In rare situations this could be a breaking change if you were relying on this to force a value
- The `stroke-width` attribute is now deprecated in favor of [styling with css](https://tailwindcss.com/docs/stroke-width) ([#192](https://github.com/xt0rted/heroicons-tag-helper/pull/192))
- Switched from [actions/setup-dotnet](https://github.com/actions/setup-dotnet) to [xt0rted/setup-dotnet](https://github.com/xt0rted/setup-dotnet) ([#144](https://github.com/xt0rted/heroicons-tag-helper/pull/144))

## [1.0.6](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.5...v1.0.6) - 2022-03-15

- Bumped `heroicons` from 1.0.5 to 1.0.6 ([#131](https://github.com/xt0rted/heroicons-tag-helper/pull/131))
  - They moved the `stroke-width` attribute from the `path` to the `svg` element in this version. This attribute is still supported, but styling with css is the preferred method now.
- Target .NET Core 3.1, .NET 5.0, and .NET 6.0 ([#95](https://github.com/xt0rted/heroicons-tag-helper/pull/95))
- Moved build scripts over to the [run-script](https://github.com/xt0rted/dotnet-run-script) dotnet tool ([#136](https://github.com/xt0rted/heroicons-tag-helper/pull/136))

## [1.0.5](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.4...v1.0.5) - 2021-11-07

- Include `README.md` in the nupkg
- Target .NET Core 3.1 and .NET 5.0
- Bumped `heroicons` from 1.0.4 to 1.0.5

## [1.0.4](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.2...v1.0.4) - 2021-08-19

- Bumped `heroicons` from 1.0.2 to 1.0.4

## [1.0.2](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.1...v1.0.2) - 2021-07-23

- Bumped `heroicons` from 1.0.1 to 1.0.2

## [1.0.1](https://github.com/xt0rted/heroicons-tag-helper/compare/v1.0.0...v1.0.1) - 2021-04-21

- Bumped `heroicons` from 1.0.0 to 1.0.1

## [1.0.0](https://github.com/xt0rted/heroicons-tag-helper/compare/v0.4.2...v1.0.0) - 2021-04-21

- Bumped `heroicons` from 0.4.2 to 1.0.0

## [0.4.2](https://github.com/xt0rted/heroicons-tag-helper/releases/tag/v0.4.2) - 2021-04-20

- Set `heroicons` version to v0.4.2
- Added outline icons
- Added solid icons
- Added settings for `IncludeComments`, `SetAccessibilityAttributes`, and `SetFocusableAttribute`
