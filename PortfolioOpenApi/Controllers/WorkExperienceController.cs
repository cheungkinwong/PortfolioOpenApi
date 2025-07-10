using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

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
    public IActionResult Get() => Ok(_context.WorkExperiences.ToList());

    [HttpPost]
    public IActionResult Create(WorkExperience experience)
    {
        _context.WorkExperiences.Add(experience);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = experience.Id }, experience);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WorkExperience updated)
    {
        var existing = _context.WorkExperiences.Find(id);
        if (existing == null) return NotFound();

        existing.Company = updated.Company;
        existing.Position = updated.Position;
        existing.Description = updated.Description;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;
        existing.Image = updated.Image;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.WorkExperiences.Find(id);
        if (item == null) return NotFound();

        _context.WorkExperiences.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}