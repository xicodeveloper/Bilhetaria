using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Models.Keywords;

public class MovieSearchResponse
{
    [JsonPropertyName("results")]
    public List<MovieResult> Results { get; set; }
}