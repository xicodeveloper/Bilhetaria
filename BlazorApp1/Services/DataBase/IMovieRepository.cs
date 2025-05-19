// Services/DataBase/IMovieRepository.cs
using BlazorApp1.Services.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Services.DataBase
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Film>> GetAllAsync();
        Task<Film> GetByIdAsync(int id);
        Task AddAsync(Film film);
        Task UpdateAsync(Film film);
        Task DeleteAsync(int id);
    }
}