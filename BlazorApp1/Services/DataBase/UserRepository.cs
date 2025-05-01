// Services/Users/Repositories/UserRepository.cs
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.DataBase
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => 
                    u.Username == usernameOrEmail || 
                    u.Email == usernameOrEmail);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public void AddAddress(User user, Adress address)
        {
            if (user.Addresses == null)
            {
                user.Addresses = new List<Adress>();
            }

            address.UserId = user.Id;
            user.Addresses.Add(address);

            _context.Addresses.Add(address); // Adiciona diretamente à tabela de endereços
            _context.SaveChanges(); // Grava na base de dados
        }


        // Método para buscar user com moradas
        public async Task<User> GetByIdAddressAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Addresses) // Carrega as moradas
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task UpdateUserAddressesAsync(User user)
        {
            // Atualizar todas as moradas do usuário
            foreach (var address in user.Addresses)
            {
                _context.Entry(address).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }




    }
}