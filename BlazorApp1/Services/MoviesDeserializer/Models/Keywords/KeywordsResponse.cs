using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Models.Keywords;

public class KeywordsResponse
{
    [JsonPropertyName("id")]
    public int MovieId { get; set; }

    [JsonPropertyName("keywords")]
    public List<KeywordInfo> Keywords { get; set; }
}