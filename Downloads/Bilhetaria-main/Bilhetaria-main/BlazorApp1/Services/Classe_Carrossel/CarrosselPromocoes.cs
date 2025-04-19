// using System.Configuration;
// using BlazorApp1.Components.Pages;
// using BlazorApp1.Services.Movies;
//
// namespace BlazorApp1.Services.Classe_Carrossel;
//
// public class CarrosselPromocoes : ICarrossel
// {
//     private string ApIlink => $"{Configuration["TmdbApi:BaseUrl"]}/movie/popular"; // Pode mudar dependendo da API de promoções
//     private MovieDeserializer deserializer = new MovieDeserializer();
//
//     public async Task<List<Film>> ObterFilmes()
//     {
//         try
//         {
//             var response = deserializer.FetchPopularMovies(ApIlink); // Aqui você pode usar uma API diferente para promoções.
//             return response.Results; // Para simular promoções, você pode aplicar um filtro ou lógica adicional.
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Erro: {ex.Message}");
//             return new List<Film>();
//         }
//     }
// }