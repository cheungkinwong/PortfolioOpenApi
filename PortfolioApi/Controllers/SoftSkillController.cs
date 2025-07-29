using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

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
    [AllowAnonymous]

    public IActionResult Get() => Ok(_context.SoftSkills.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.SoftSkills.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult Create(SoftSkill skill)
    {
        skill.Id = 0;
        skill.SectionId = 5;
        skill.Section = null;
        _context.SoftSkills.Add(skill);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

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
    [Authorize(Roles = "Admin")]

    public IActionResult Delete(int id)
    {
        var item = _context.SoftSkills.Find(id);
        if (item == null) return NotFound();

        _context.SoftSkills.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}