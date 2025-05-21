using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Services.DataBase.DBEntities;
public class User :  DbItem
{
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsSucess { get; set; }

    // 1:N de endereços
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();


     public virtual WalletUser Wallet { get; set; }

    public User() { }

    public User(string username, string email, string passwordHash)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
}