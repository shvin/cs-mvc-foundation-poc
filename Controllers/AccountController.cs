using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CsMvcFoundationPoc.Controllers;

/// <summary>
/// Minimal login/logout for production auth.
/// In Development, UseWebShellSimulatedAuth() handles identity via cookie
/// so this controller is only reached in non-dev environments.
/// </summary>
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
    {
        // TODO: replace with real credential validation against your IDP
        if (username == "admin" && password == "demo")
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name,  username),
                new(ClaimTypes.Email, $"{username}@cs.internal"),
                new(ClaimTypes.Role,  "Admin"),
            };

            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return LocalRedirect(returnUrl ?? "/");
        }

        ModelState.AddModelError(string.Empty, "Invalid credentials.");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
