using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

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
    public IActionResult Get() => Ok(_context.Hobbies.ToList());

    [HttpPost]
    public IActionResult Create(Hobby hobby)
    {
        _context.Hobbies.Add(hobby);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = hobby.Id }, hobby);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Hobby updated)
    {
        var existing = _context.Hobbies.Find(id);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;
        existing.Image = updated.Image;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.Hobbies.Find(id);
        if (item == null) return NotFound();

        _context.Hobbies.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}
