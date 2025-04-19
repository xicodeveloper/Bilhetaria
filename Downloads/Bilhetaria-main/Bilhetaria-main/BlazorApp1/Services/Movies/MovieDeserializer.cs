using Newtonsoft.Json;
using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp1.Services.Movies;

public class MovieDeserializer :  IDisposable
{
    private HttpClient? _httpClient;

    public MovieResponse FetchPopularMovies(string link)
    {
        _httpClient = new HttpClient(); // Mantém uma instância reutilizável
        
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key não configurada no ambiente.");
        }
        
        var endpoint = new Uri(link + "?api_key=" + apiKey);
        var response = _httpClient.GetAsync(endpoint).Result;
        string jsonString = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<MovieResponse>(jsonString);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

}