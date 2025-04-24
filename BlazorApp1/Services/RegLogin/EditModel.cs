using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

namespace BlazorApp1.Services.RegLogin;
public class EditModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome de utilizador obrigatório")]
    [StringLength(20, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
}