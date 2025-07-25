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