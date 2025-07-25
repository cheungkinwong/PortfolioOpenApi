namespace PortfolioApi.Models;

public class Section
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }

    public ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();
    public ICollection<SoftSkill> SoftSkills { get; set; } = new List<SoftSkill>();
    public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
}
