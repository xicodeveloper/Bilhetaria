// Services/DataBase/MovieRepository.cs
using BlazorApp1.Services.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Services.DataBase
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task AddAsync(Film film)
        {
            await _context.Movies.AddAsync(film);
        }

        public async Task UpdateAsync(Film film)
        {
            _context.Movies.Update(film);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var film = await GetByIdAsync(id);
            if (film != null)
            {
                _context.Movies.Remove(film);
            }
        }
    }
}