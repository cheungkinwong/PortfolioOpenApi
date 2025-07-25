using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioApi.Controllers;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkExperienceController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<WorkExperienceController> _logger;

    public WorkExperienceController(ApplicationDbContext context, ILogger<WorkExperienceController> logger)
    {
        _context = context;
        _logger = logger;
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
        _context.WorkExperiences.Add(experience);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = experience.Id }, experience);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Update(int id, WorkExperience updated)
    {
        _logger.LogInformation("Attempting to update WorkExperience with ID: {Id}", id);
        var existing = _context.WorkExperiences.Find(id);
        if (existing == null)
        {
            _logger.LogWarning("WorkExperience with ID {Id} not found", id);
            return NotFound();
        }
        existing.Company = updated.Company;
        existing.Position = updated.Position;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;

        _context.SaveChanges();
        _logger.LogInformation("Successfully updated WorkExperience with ID: {Id}", id);
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