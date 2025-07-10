using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoftSkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SoftSkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.SoftSkills.ToList());

    [HttpPost]
    public IActionResult Create(SoftSkill skill)
    {
        _context.SoftSkills.Add(skill);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, SoftSkill updated)
    {
        var existing = _context.SoftSkills.Find(id);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;
        existing.Level = updated.Level;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.SoftSkills.Find(id);
        if (item == null) return NotFound();

        _context.SoftSkills.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}