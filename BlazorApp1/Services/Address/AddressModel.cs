using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Services.Address;


public class AddressModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A rua é obrigatória.")]
    [StringLength(100, ErrorMessage = "A rua não pode ter mais de 100 caracteres.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "O número é obrigatório.")]
    [StringLength(10, ErrorMessage = "O número não pode ter mais de 10 caracteres.")]
    public string Number { get; set; }

    [Required(ErrorMessage = "O código postal é obrigatório.")]
    [RegularExpression(@"^\d{4}-\d{3}$", ErrorMessage = "O código postal deve estar no formato 0000-000.")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    [StringLength(50, ErrorMessage = "A cidade não pode ter mais de 50 caracteres.")]
    public string City { get; set; }

    [Required(ErrorMessage = "O distrito/estado é obrigatório.")]
    [StringLength(50, ErrorMessage = "O estado não pode ter mais de 50 caracteres.")]
    public string State { get; set; }

    [Required(ErrorMessage = "O país é obrigatório.")]
    [StringLength(50, ErrorMessage = "O país não pode ter mais de 50 caracteres.")]
    public string Country { get; set; }

    public int UserId { get; set; }
}