namespace PortfolioApi.Models;


public class WorkExperience
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 3;
    public Section Section { get; set; } = null!;
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
