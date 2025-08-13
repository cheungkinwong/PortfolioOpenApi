using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Controllers;
public class CVController : Controller
{
    [HttpPost("api/cv/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadCv(IFormFile file, [FromServices] IWebHostEnvironment env)
    {

        try
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
            var rootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var folder = Path.Combine(env.ContentRootPath, "App_Data", "files");

            Directory.CreateDirectory(folder);

            var existingCvPath = Path.Combine(folder, "cv-cheungkinwong.pdf");
            if (System.IO.File.Exists(existingCvPath))
            {
                System.IO.File.Delete(existingCvPath);
            }

            var filePath = Path.Combine(folder, "cv-cheungkinwong.pdf");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = "CV uploaded successfully", path = "/files/cv-cheungkinwong.pdf" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex}");
            return StatusCode(500, new { error = ex.Message });
        }
    }


    [HttpGet("api/cv/download")]
    [AllowAnonymous]
    public IActionResult DownloadCv([FromServices] IWebHostEnvironment env)
    {
        try
        {
            var folder = Path.Combine(env.ContentRootPath, "App_Data", "files");

            var filePath = Path.Combine(folder, "cv-cheungkinwong.pdf");

            if (!System.IO.File.Exists(filePath))
                return NotFound("CV not found.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            Response.Headers.Add("Content-Disposition", "attachment; filename=\"cv-cheungkinwong.pdf\"");
            return File(fileBytes, "application/pdf", "cv-cheungkinwong.pdf");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex}");
            return StatusCode(500, new { error = ex.Message });
        }
    }

}
