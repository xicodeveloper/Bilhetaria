using Newtonsoft.Json;

namespace BlazorApp1.Services.Movies;

public class MovieDeserializer
{
    public MovieResponse FetchPopularMovies(string link)
    {
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key não configurada no ambiente.");
        }
        using (var client = new HttpClient())
        {
            var endpoint = new Uri(link + "?api_key=" + apiKey);
            var response = client.GetAsync(endpoint).Result;
            string jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<MovieResponse>(jsonString);
        }
    }
}