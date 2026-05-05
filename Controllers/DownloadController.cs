using Microsoft.AspNetCore.Mvc;

namespace CsMvcFoundationPoc.Controllers;

public class DownloadController : AppController
{
    private readonly IWebHostEnvironment _env;

    public DownloadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Index()
    {
        ViewData["Title"]     = "Downloads";
        ViewData["ActiveNav"] = "downloads";

        var cssPath = Path.Combine(_env.WebRootPath, "css");
        var files = Directory.Exists(cssPath)
            ? Directory.GetFiles(cssPath)
                       .Select(f => Path.GetFileName(f)!)
                       .OrderBy(f => f)
                       .ToList()
            : [];

        return View(files);
    }

    public IActionResult File(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest();

        // Prevent path traversal — allow only simple filenames
        if (name.Contains('/') || name.Contains('\\') || name.Contains(".."))
            return BadRequest();

        var cssPath = Path.Combine(_env.WebRootPath, "css");
        var fullPath = Path.Combine(cssPath, name);

        // Verify the resolved path stays inside the css folder
        if (!Path.GetFullPath(fullPath).StartsWith(Path.GetFullPath(cssPath) + Path.DirectorySeparatorChar))
            return BadRequest();

        if (!System.IO.File.Exists(fullPath))
            return NotFound();

        return PhysicalFile(fullPath, "application/octet-stream", name);
    }
}
