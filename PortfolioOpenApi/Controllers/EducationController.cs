using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioOpenApi.Models;

namespace PortfolioOpenApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EducationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Educations.ToList());

    [HttpPost]
    public IActionResult Create(Education education)
    {
        _context.Educations.Add(education);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = education.Id }, education);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Education updated)
    {
        var existing = _context.Educations.Find(id);
        if (existing == null) return NotFound();

        existing.School = updated.School;
        existing.Degree = updated.Degree;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;
        existing.Image = updated.Image;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.Educations.Find(id);
        if (item == null) return NotFound();

        _context.Educations.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}
