using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

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
    public IActionResult Get() => Ok(_context.Sections.ToList());

    [HttpPut("{id}")]
    public IActionResult Update(int id, Section updated)
    {
        var section = _context.Sections.Find(id);
        if (section == null) return NotFound();

        section.Title = updated.Title;
        section.Description = updated.Description;
        section.Image = updated.Image;

        _context.SaveChanges();
        return NoContent();
    }
}