namespace BlazorApp1.Services.Movies;

using Newtonsoft.Json;
using System.Collections.Generic;

public class MovieResponse
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("results")]
    public List<Film> Results { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

