using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Actor;

public class CastMember
{
    [JsonPropertyName("cast_id")]
    public int CastId { get; set; }

    [JsonPropertyName("character")]
    public string Character { get; set; }

    [JsonPropertyName("credit_id")]
    public string CreditId { get; set; }

    [JsonPropertyName("gender")]
    public int? Gender { get; set; }

    [JsonPropertyName("id")]
    public int PersonId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }
}