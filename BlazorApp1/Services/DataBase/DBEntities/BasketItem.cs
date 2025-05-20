// Services/Orders/Models/BasketItem.cs

using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Services.DataBase.DBEntities;

public class BasketItem  : DbItem
{ 
    [Key] 
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid MovieId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public Order Order { get; set; }
    public Movie Movie { get; set; }
    public double TotalPrice => Price * Quantity;
    public int Discount { get; set; } 
}