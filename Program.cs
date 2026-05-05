using CS.Shared.Core.Navigation;
using CS.Shared.WebMvcShell.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ── Authentication ────────────────────────────────────────────────────────────
// AddAuthentication must be called before AddWebSecurity.
// AddWebSecurity applies cookie settings from appsettings.json "Security" section.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts => opts.LoginPath = "/account/login");

builder.Services.AddWebSecurity(builder.Configuration);

// ── WebShell sidebar ──────────────────────────────────────────────────────────
builder.Services.AddWebShell(opts =>
{
    // Branding
    opts.AppName    = "Project Hub";
    opts.AppTagLine = "Internal Tools";
    opts.PortalName = "CS Portal";
    opts.ThemeColor = "#1a56db";
    opts.ShowEnvironmentBadge = true;
    opts.AppIconSvg =
        "<rect x='10' y='10' width='80' height='80' rx='12' fill='white' opacity='.9'/>" +
        "<path d='M30 55 L50 35 L70 55' stroke='#1a56db' stroke-width='8' stroke-linecap='round' stroke-linejoin='round' fill='none'/>" +
        "<path d='M40 75 L40 55 L60 55 L60 75' stroke='#1a56db' stroke-width='8' stroke-linecap='round' stroke-linejoin='round' fill='none'/>";

    // Navigation
    opts.NavSections =
    [
        new NavSection
        {
            Label = "Main",
            Items =
            [
                new NavItem
                {
                    Label   = "Dashboard",
                    Href    = "/",
                    NavKey  = "dashboard",
                    IconSvg = "<path d='M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
                new NavItem
                {
                    Label   = "Projects",
                    Href    = "/projects",
                    NavKey  = "projects",
                    IconSvg = "<path d='M3 7a2 2 0 012-2h4l2 2h8a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2V7z' stroke='currentColor' stroke-width='1.5' stroke-linejoin='round'/>",
                },
                new NavItem
                {
                    Label   = "Tasks",
                    Href    = "/tasks",
                    NavKey  = "tasks",
                    IconSvg = "<path d='M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-6 9l2 2 4-4' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
            ]
        },
        new NavSection
        {
            Label = "Files",
            Items =
            [
                new NavItem
                {
                    Label   = "Downloads",
                    Href    = "/download",
                    NavKey  = "downloads",
                    IconSvg = "<path d='M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
            ]
        },
        new NavSection
        {
            Label = "Analytics",
            Items =
            [
                new NavItem
                {
                    Label        = "Reports",
                    Href         = "/reports",
                    NavKey       = "reports",
                    AllowedRoles = ["Admin", "Manager"],
                    Description  = "Usage and activity reports",
                    IconSvg      = "<path d='M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
                new NavItem
                {
                    Label        = "Insights",
                    Href         = "/insights",
                    NavKey       = "insights",
                    AllowedRoles = ["Admin"],
                    Description  = "AI-powered project insights",
                    IconSvg      = "<path d='M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
            ]
        },
        new NavSection
        {
            Label = "Settings",
            Items =
            [
                new NavItem
                {
                    Label        = "Users",
                    Href         = "/settings/users",
                    NavKey       = "settings-users",
                    AllowedRoles = ["Admin"],
                    IconSvg      = "<path d='M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0z' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'/>",
                },
                new NavItem
                {
                    Label        = "System",
                    Href         = "/settings/system",
                    NavKey       = "settings-system",
                    AllowedRoles = ["Admin"],
                    IconSvg      = "<path d='M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z' stroke='currentColor' stroke-width='1.5'/><circle cx='12' cy='12' r='3' stroke='currentColor' stroke-width='1.5'/>",
                },
            ]
        },
    ];

    // User info fallbacks (controllers override per-request via ViewData)
    opts.DefaultUserName  = "Demo User";
    opts.DefaultUserEmail = "demo@cs.internal";
    opts.DefaultOrgName   = "CS Team";

    // Dev: role switcher widget in sidebar
    opts.ShowRoleSwitcher = builder.Environment.IsDevelopment();
    opts.SimulatedRoles   = ["Admin", "Manager", "Viewer"];
});

// ── App pipeline ──────────────────────────────────────────────────────────────
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Must come before UseAuthentication so the simulated principal is in place
if (app.Environment.IsDevelopment())
    app.UseWebShellSimulatedAuth();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
