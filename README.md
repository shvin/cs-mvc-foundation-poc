# cs-mvc-foundation-poc

Sample ASP.NET Core MVC application demonstrating integration of the
[cs-shared-libs](https://github.com/shvin/cs-shared-libs) foundation packages.

## What this POC demonstrates

| Feature | Where to look |
|---|---|
| WebShell sidebar layout | `Views/_ViewStart.cshtml`, `Program.cs` |
| Branding (app name, icon, tagline) | `Program.cs` → `AddWebShell` |
| Multi-section navigation | `Program.cs` → `opts.NavSections` |
| Active nav highlighting | Each controller action sets `ViewData["ActiveNav"]` |
| RBAC nav filtering | `AllowedRoles` on `NavItem` in `Program.cs` |
| Role-based page access | `[Authorize(Roles="Admin,Manager")]` on controllers |
| Per-request user info | `AppController.cs` base controller |
| Dev role switcher | `opts.ShowRoleSwitcher` + `UseWebShellSimulatedAuth()` |
| Cookie auth config | `appsettings.json` → `"Security"` section |
| AI disclaimer footer | `Views/Home/Insights.cshtml` → `@section Footer` |
| Theme switching | Sidebar → account dropdown → Themes |

## Project structure

```
Controllers/
  AppController.cs       ← base controller: populates user ViewData from claims
  HomeController.cs      ← Dashboard, Projects, Tasks, Insights
  ReportsController.cs   ← [Authorize(Roles="Admin,Manager")]
  SettingsController.cs  ← [Authorize(Roles="Admin")]
  AccountController.cs   ← Login / Logout (used in non-dev environments)
Views/
  _ViewStart.cshtml      ← sets Layout = "_WebShellLayout" for all views
  Home/                  ← Dashboard, Projects, Tasks, Insights
  Reports/               ← RBAC-protected report list
  Settings/              ← Admin-only Users and System pages
  Account/               ← Login page (plain layout, no sidebar)
wwwroot/css/site.css     ← app-specific styles (WebShell CSS vars available here)
Program.cs               ← full WebShell + WebSecurity wiring
appsettings.json         ← Security cookie config
```

## Getting started

### Prerequisites

- .NET 8 SDK
- Both repos cloned **side by side** in the same parent folder:

```
parent/
├── cs-shared-libs/           ← https://github.com/shvin/cs-shared-libs
└── cs-mvc-foundation-poc/    ← this repo
```

The `.csproj` uses relative `ProjectReference` paths that assume this layout.
Once NuGet packages are published from `cs-shared-libs`, swap to
`PackageReference` (commented out in the `.csproj`).

### Run

```bash
cd cs-mvc-foundation-poc
dotnet run
# Browse to http://localhost:5000
```

In Development the sidebar role switcher is visible at the bottom of the
sidebar. Switch between **Admin**, **Manager**, and **Viewer** to see nav
items appear and disappear based on `AllowedRoles`.

| Role | Can see |
|---|---|
| Admin | All nav items, Reports, Insights, Users, System |
| Manager | Dashboard, Projects, Tasks, Reports, Insights |
| Viewer | Dashboard, Projects, Tasks only |

### Switch to NuGet packages

Once `cs-shared-libs` has had a `v*` tag pushed (which triggers the publish
pipeline), replace the `ProjectReference` block in the `.csproj` with:

```xml
<PackageReference Include="CS.Shared.Web.Mvc.WebShell"   Version="1.*" />
<PackageReference Include="CS.Shared.Web.Mvc.WebSecurity" Version="1.*" />
```

And add credentials to `nuget.config` (or your global NuGet config):

```
NUGET_USERNAME=<your GitHub username>
NUGET_TOKEN=<GitHub PAT with read:packages>
```
