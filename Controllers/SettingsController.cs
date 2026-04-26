using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsMvcFoundationPoc.Controllers;

[Authorize(Roles = "Admin")]
public class SettingsController : AppController
{
    public IActionResult Users()
    {
        ViewData["Title"]     = "Users";
        ViewData["ActiveNav"] = "settings-users";
        return View();
    }

    public IActionResult System()
    {
        ViewData["Title"]     = "System";
        ViewData["ActiveNav"] = "settings-system";
        return View();
    }
}
