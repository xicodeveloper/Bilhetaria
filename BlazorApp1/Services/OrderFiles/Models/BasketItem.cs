// Services/Orders/Models/BasketItem.cs

using System.ComponentModel.DataAnnotations;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.Orders.Models;

public class BasketItem
{
    [Key] 
    public int Id { get; set; } // FK
    public int BasketId { get; set; }

    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public int Quantity { get; set; }
    public TicketType Type { get; set; }
    public DateTime? ScreeningDate { get; set; }
    public string MoviePosterUrl { get; set; }
    public double Price { get; set; }
    public Basket Basket { get; set; }
    public double TotalPrice => Price * Quantity;
    public int Discount { get; set; } 
    // public string Genre { get; set; }
}