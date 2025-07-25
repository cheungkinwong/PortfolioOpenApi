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
}