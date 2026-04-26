using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsMvcFoundationPoc.Controllers;

/// <summary>
/// Demonstrates RBAC — only Admin and Manager roles can access these pages.
/// The nav item is also hidden from other roles via NavItem.AllowedRoles.
/// </summary>
[Authorize(Roles = "Admin,Manager")]
public class ReportsController : AppController
{
    public IActionResult Index()
    {
        ViewData["Title"]     = "Reports";
        ViewData["ActiveNav"] = "reports";
        return View();
    }
}
