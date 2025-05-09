using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services;
using BlazorApp1.Services.Orders.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsSucess { get; set; }

    // 1:N de endereços
    public virtual ICollection<Adress> Addresses { get; set; } = new List<Adress>();


     public virtual WalletUser Wallet { get; set; }

    public User() { }

    public User(string username, string email, string passwordHash)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
}