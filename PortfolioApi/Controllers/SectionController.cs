using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get() => Ok(_context.Sections.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.Sections.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Section updated)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section == null) return NotFound();

        section.Title = updated.Title;
        section.Description = updated.Description;
        section.Image = updated.Image;
        section.AltText = updated.AltText;

        await _context.SaveChangesAsync();
        return Ok(section);
    }

    [HttpPost("{id}/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file, [FromForm] string altText, [FromServices] IWebHostEnvironment env)
    {
        var section = _context.Sections.Find(id);
        if (section == null) return NotFound();

        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Unsupported file type.");

        if (file.Length > 5 * 1024 * 1024) 
            return BadRequest("File too large (max 5 MB).");

        string uploadsFolder = Path.Combine(
          env.IsDevelopment() ? Directory.GetCurrentDirectory() : env.WebRootPath,
          "images");

        Directory.CreateDirectory(uploadsFolder);

        if (!string.IsNullOrEmpty(section.Image))
        {
            var oldFileName = Path.GetFileName(section.Image); 
            var oldPath = Path.Combine(uploadsFolder, oldFileName);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
        }


        var uniqueFileName = $"section-{id}-{Guid.NewGuid()}{extension}"; var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        section.Image = $"/images/{uniqueFileName}";
        section.AltText = altText;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            section.Id,
            image = section.Image,
            altText = section.AltText
        });
    }

}