
using BlazorApp1.Components.Pages;

namespace BlazorApp1.Services.OrderFiles;

public class Order
{
    public int UserId { get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }
    
    public Basket Basket { get; set; }
    
    
    public Adress ShippingAddress { get; set; } // Fixed property name
}