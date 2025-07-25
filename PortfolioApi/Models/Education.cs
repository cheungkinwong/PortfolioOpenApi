namespace PortfolioApi.Models;

public class Education
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 2;
    public Section Section { get; set; } = null!;
    public string School { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

