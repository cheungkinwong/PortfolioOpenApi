using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Controllers;
public class CVController : Controller
{
    [HttpPost("cv/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadCv(IFormFile file, [FromServices] IWebHostEnvironment env)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var allowedExtensions = new[] { ".pdf" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Only PDF files are allowed.");

        if (file.Length > 10 * 1024 * 1024)
            return BadRequest("File too large (max 10 MB).");

        var isDev = env.IsDevelopment();
        var folder = isDev
            ? Path.Combine(Directory.GetCurrentDirectory(), "files")
            : Path.Combine(env.WebRootPath, "files");

        Directory.CreateDirectory(folder);

        var existingCvPath = Path.Combine(folder, "cv.pdf");
        if (System.IO.File.Exists(existingCvPath))
        {
            System.IO.File.Delete(existingCvPath);
        }

        var filePath = Path.Combine(folder, "cv.pdf");
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { message = "CV uploaded successfully", path = "/files/cv.pdf" });
    }


    [HttpGet("cv/download")]
    [AllowAnonymous]
    public IActionResult DownloadCv([FromServices] IWebHostEnvironment env)
    {
        var isDev = env.IsDevelopment();

        var rootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var folder = Path.Combine(rootPath, "files");

        var filePath = Path.Combine(folder, "cv.pdf");

        if (!System.IO.File.Exists(filePath))
            return NotFound("CV not found.");

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(stream, "application/pdf", "cv.pdf");
    }

}
