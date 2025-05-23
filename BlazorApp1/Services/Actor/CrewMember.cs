using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Actor;

public class CrewMember
{
    [JsonPropertyName("credit_id")]
    public string CreditId { get; set; }

    [JsonPropertyName("department")]
    public string Department { get; set; }

    [JsonPropertyName("gender")]
    public int? Gender { get; set; }

    [JsonPropertyName("id")]
    public int PersonId { get; set; }

    [JsonPropertyName("job")]
    public string Job { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }
}