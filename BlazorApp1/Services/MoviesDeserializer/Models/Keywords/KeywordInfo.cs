using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Models.Keywords;

public class KeywordInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}