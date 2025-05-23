using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Actor;
public class CreditsResponse
{
    [JsonPropertyName("id")]
    public int MovieId { get; set; }

    [JsonPropertyName("cast")]
    public List<CastMember> Cast { get; set; }

    [JsonPropertyName("crew")]
    public List<CrewMember> Crew { get; set; }
}