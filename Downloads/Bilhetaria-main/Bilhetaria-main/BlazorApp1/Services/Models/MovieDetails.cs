using System.Text.Json.Serialization;

namespace BlazorApp1.Services.Models;

public class MovieDetails
{
    public int Id { get; set; }

    public string Title { get; set; }

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; }

    public string Overview { get; set; }

    public string Tagline { get; set; }

    public int Runtime { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    public List<Genre> Genres { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("release_date")]
    public object ReleaseDate { get; set; }
}

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }
}