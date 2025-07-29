using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HobbyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HobbyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]

    public IActionResult Get() => Ok(_context.Hobbies.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.Hobbies.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult Create(Hobby hobby)
    {
        hobby.Id = 0;
        hobby.SectionId = 6;
        hobby.Section = null;
        _context.Hobbies.Add(hobby);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = hobby.Id }, hobby);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Update(int id, Hobby updated)
    {
        var existing = _context.Hobbies.Find(id);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Delete(int id)
    {
        var item = _context.Hobbies.Find(id);
        if (item == null) return NotFound();

        _context.Hobbies.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}
