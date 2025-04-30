namespace BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Movies;
public class Basket
{
    public int Id { get; set; } 
    
    public List<BasketItem> Items { get; set; } = new();
    public int TotalItems => Items.Sum(i => i.Quantity);
}