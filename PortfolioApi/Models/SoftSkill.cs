using System.Text.Json.Serialization;

namespace PortfolioApi.Models;

public class SoftSkill
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 5;
    [JsonIgnore]
    public Section? Section { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Level { get; set; }
}
