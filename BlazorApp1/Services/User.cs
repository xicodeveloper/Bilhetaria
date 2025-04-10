using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Parameterless constructor REQUIRED for Entity Framework
        public User()
        {
        }

        // Optional constructor for convenience
        public User(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
