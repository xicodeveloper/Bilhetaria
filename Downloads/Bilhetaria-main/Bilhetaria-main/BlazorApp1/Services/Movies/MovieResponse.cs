using System.Text.Json.Serialization;
using BlazorApp1.Services.Models;

namespace BlazorApp1.Services.Movies;

using Newtonsoft.Json;
using System.Collections.Generic;

// A FALTAR:
// O results A MAIS, que eu não me recordo se fui eu que fiz ou não;

public class MovieResponse
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("results")]
    public List<Film>? Results { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

