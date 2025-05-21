// Services/Orders/Models/BasketItem.cs

using System.ComponentModel.DataAnnotations;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.DBEntities;

public abstract class BasketItem  : DbItem
{ 

    public Guid OrderId { get; set; }
    public Guid MovieId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public Order Order { get; set; }
    public Movie Movie { get; set; }
    public int Discount { get; set; }
    public double TotalPrice => Price * Quantity;
    public abstract TicketType GetTicketType();
    
}