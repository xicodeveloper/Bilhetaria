using BlazorApp1.Services.Movies;

namespace BlazorApp1.Components.Pages;

public interface ICarrossel
{
    Task<List<Film>> ObterFilmes();
}