namespace PortfolioOpenApi.Models;

public class Hobby
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 6;
    public Section Section { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
}

