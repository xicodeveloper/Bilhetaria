using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Models.Keywords;

public class MovieResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}
