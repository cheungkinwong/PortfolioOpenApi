using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]

    public IActionResult Get() => Ok(_context.Projects.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.Projects.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult Create(Project project)
    {
        _context.Projects.Add(project);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Update(int id, Project updated)
    {
        var existing = _context.Projects.Find(id);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.Link = updated.Link;
        existing.Image = updated.Image;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Delete(int id)
    {
        var item = _context.Projects.Find(id);
        if (item == null) return NotFound();

        _context.Projects.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file, [FromForm] string altText, [FromServices] IWebHostEnvironment env)
    {
        try
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Unsupported file type.");

            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("File too large (max 5 MB).");

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");

            Directory.CreateDirectory(uploadsFolder);

            if (!string.IsNullOrEmpty(project.Image))
            {
                var oldImageRelativePath = project.Image.TrimStart('/');
                var oldPath = Path.Combine(uploadsFolder, oldImageRelativePath.Replace("images/", ""));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            var uniqueFileName = $"project-{id}-{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            project.Image = $"/images/{uniqueFileName}";
            project.AltText = altText;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                project.Id,
                image = project.Image,
                altText = project.AltText
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Upload failed: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new { error = "Internal server error", detail = ex.Message });
        }
    }

}