using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkExperienceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WorkExperienceController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get() => Ok(_context.WorkExperiences.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.WorkExperiences.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult Create(WorkExperience experience)
    {
        experience.Id = 0;
        experience.SectionId = 3;
        experience.Section = null;
        _context.WorkExperiences.Add(experience);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = experience.Id }, experience);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Update(int id, WorkExperience updated)
    {
        var existing = _context.WorkExperiences.Find(id);
        if (existing == null)
        {
            return NotFound();
        }
        existing.Company = updated.Company;
        existing.Position = updated.Position;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Delete(int id)
    {
        var item = _context.WorkExperiences.Find(id);
        if (item == null) return NotFound();

        _context.WorkExperiences.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}