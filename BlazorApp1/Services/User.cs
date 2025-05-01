// User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsSucess { get; set; }

        // Adicione uma lista de endereços associados ao user
        public virtual ICollection<Adress> Addresses { get; set; } = new List<Adress>();

        public User() { }

        public User(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}