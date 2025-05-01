using System.ComponentModel.DataAnnotations;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.Orders.Models;

public class Basket
{
    [Key]
    public int Id { get; set; }
    public List<BasketItem> Items { get; set; } = new();
    public int TotalItems => Items.Sum(i => i.Quantity);
    public double TotalPrice => Items.Sum(i => i.TotalPrice);
}