using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CsMvcFoundationPoc.Controllers;

/// <summary>
/// Base controller that populates sidebar user info from claims on every request.
/// All app controllers inherit this instead of Controller directly.
/// </summary>
public abstract class AppController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewData["UserName"]  = User.FindFirstValue(ClaimTypes.Name)  ?? "Guest";
        ViewData["UserEmail"] = User.FindFirstValue(ClaimTypes.Email) ?? "";
        ViewData["OrgName"]   = User.FindFirstValue("organisation")   ?? "CS Team";
        base.OnActionExecuting(context);
    }
}
