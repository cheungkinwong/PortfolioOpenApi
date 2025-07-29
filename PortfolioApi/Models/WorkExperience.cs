using System.Text.Json.Serialization;

namespace PortfolioApi.Models;


public class WorkExperience
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 3;
    [JsonIgnore]
    public Section? Section { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
