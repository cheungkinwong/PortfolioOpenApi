using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

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
    [AllowAnonymous]

    public IActionResult Get() => Ok(_context.Educations.ToList());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var section = _context.Educations.Find(id);
        if (section == null) return NotFound();
        return Ok(section);
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult Create(Education education)
    {
        _context.Educations.Add(education);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = education.Id }, education);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Update(int id, Education updated)
    {
        var existing = _context.Educations.Find(id);
        if (existing == null) return NotFound();

        existing.School = updated.School;
        existing.Course = updated.Course;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public IActionResult Delete(int id)
    {
        var item = _context.Educations.Find(id);
        if (item == null) return NotFound();

        _context.Educations.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }
}
