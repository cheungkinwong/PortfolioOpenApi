using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechnicalSkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TechnicalSkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.TechnicalSkills.ToList());

    [HttpPost]
    public IActionResult Create(TechnicalSkill skill)
    {
        _context.TechnicalSkills.Add(skill);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TechnicalSkill updated)
    {
        var existing = _context.TechnicalSkills.Find(id);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.TechnicalSkills.Find(id);
        if (item == null) return NotFound();

        _context.TechnicalSkills.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}