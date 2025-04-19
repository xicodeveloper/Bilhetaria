// using System.Configuration;
// using BlazorApp1.Components.Pages;
// using BlazorApp1.Services.Movies;
//
// namespace BlazorApp1.Services.Classe_Carrossel;
//
// public class CarrosselPopulares : ICarrossel
// {
//     private string ApIlink => $"{Configuration["TmdbApi:BaseUrl"]}/movie/popular";
//     private MovieDeserializer deserializer = new MovieDeserializer();
//
//     public async Task<List<Film>> ObterFilmes()
//     {
//         try
//         {
//             var response = deserializer.FetchPopularMovies(ApIlink);
//             return response.Results;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Erro: {ex.Message}");
//             return new List<Film>();
//         }
//     }
// }
