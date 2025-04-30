using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services.OrderFiles;

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

        private List<Adress> _addresses { get; } = new List<Adress>();
        public bool IsSucess { get; set; }
        

        // Parameterless constructor REQUIRED for Entity Framework
        public User()
        {
        }
        public void addAdress(Adress adress)
        {
            _addresses.Add(adress);
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
