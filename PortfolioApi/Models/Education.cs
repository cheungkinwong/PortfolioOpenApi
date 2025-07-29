using System.Text.Json.Serialization;

namespace PortfolioApi.Models;

public class Education
{
    public int Id { get; set; }
    public int SectionId { get; set; } = 2;
    [JsonIgnore]
    public Section? Section { get; set; }
    public string School { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

