using System.ComponentModel.DataAnnotations;

public class RegistrationModel
{
    
    [Required(ErrorMessage = "Username é obrigatório")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Username deve ter entre 3-20 caracteres")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Senha é obrigatória")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", 
        ErrorMessage = "Senha deve ter mínimo 6 caracteres, 1 letra maiúscula, 1 minúscula e 1 número")]
    public string Password { get; set; } = "";
}