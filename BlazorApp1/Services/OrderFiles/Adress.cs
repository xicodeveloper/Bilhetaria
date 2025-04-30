// Adress.cs
namespace BlazorApp1.Services.OrderFiles
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // Chave estrangeira para o User
        public int UserId { get; set; }

        // Propriedade de navegação para o User
        public virtual User User { get; set; }
    }
}