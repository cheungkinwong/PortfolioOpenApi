using System.Text.Json.Serialization;

namespace PortfolioApi.Models;

public class TechnicalSkill
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 4;
    [JsonIgnore]
    public Section? Section { get; set; }
    public string Name { get; set; } = string.Empty;
}

