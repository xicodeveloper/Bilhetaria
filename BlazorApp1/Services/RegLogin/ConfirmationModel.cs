using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Services.RegLogin;


public class ConfirmationModel
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Código de confirmação é obrigatório")]
    [StringLength(6, ErrorMessage = "O código deve ter 6 dígitos", MinimumLength = 6)]
    public string Code { get; set; }
}