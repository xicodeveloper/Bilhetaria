using Newtonsoft.Json;
namespace BlazorApp1.Services.Movies;

public class Film
{

    [JsonProperty("adult")]
    public bool Adult { get; set; }

    [JsonProperty("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonProperty("genre_ids")]
    // public List<int> GenreIds { get; set; }
    [JsonIgnore]
    public string GenreIdsSerialized
    {
        get => JsonConvert.SerializeObject(GenreIds);
        set => GenreIds = JsonConvert.DeserializeObject<List<int>>(value) ?? new List<int>();
    }
    public List<int> GenreIds { get; set; } = new List<int>();

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("original_language")]
    public string OriginalLanguage { get; set; }

    [JsonProperty("original_title")]
    public string OriginalTitle { get; set; }

    [JsonProperty("overview")]
    public string Overview { get; set; }

    [JsonProperty("popularity")]
    public double Popularity { get; set; }

    [JsonProperty("poster_path")]
    public string PosterPath { get; set; }

    [JsonProperty("release_date")]
    public DateTime ReleaseDate { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("video")]
    public bool Video { get; set; }

    [JsonProperty("vote_average")]
    public double VoteAverage { get; set; }

    [JsonProperty("vote_count")]
    public int VoteCount { get; set; }
    public String? Json { get; set; }
 
}


