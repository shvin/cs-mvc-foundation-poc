using Microsoft.AspNetCore.Mvc;

namespace CsMvcFoundationPoc.Controllers;

public class HomeController : AppController
{
    public IActionResult Index()
    {
        ViewData["Title"]     = "Dashboard";
        ViewData["ActiveNav"] = "dashboard";
        return View();
    }

    public IActionResult Projects()
    {
        ViewData["Title"]     = "Projects";
        ViewData["ActiveNav"] = "projects";
        return View();
    }

    public IActionResult Tasks()
    {
        ViewData["Title"]     = "Tasks";
        ViewData["ActiveNav"] = "tasks";
        return View();
    }

    public IActionResult Insights()
    {
        ViewData["Title"]     = "Insights";
        ViewData["ActiveNav"] = "insights";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
