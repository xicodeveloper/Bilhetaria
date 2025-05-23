using BlazorApp1.Services.Models.Keywords;

public class MovieService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public MovieService(HttpClient http, string apiKey)
    {
        _http = http;
        _apiKey = apiKey;
    }

    public async Task<int?> GetMovieIdByNameAsync(string name)
    {
        var url = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(name)}";
        var response = await _http.GetFromJsonAsync<MovieSearchResponse>(url);
        return response?.Results?.FirstOrDefault()?.Id;
    }
}