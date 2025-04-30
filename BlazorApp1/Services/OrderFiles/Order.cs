namespace BlazorApp1.Services.OrderFiles;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; } // Chave estrangeira
    public int Number { get; set; }
    public DateTime Date { get; set; }

    
    // Relacionamentos
    public Basket Basket { get; set; }
    public Adress ShippingAddress { get; set; }
    

}