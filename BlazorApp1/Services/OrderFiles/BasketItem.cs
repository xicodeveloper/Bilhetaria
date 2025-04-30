namespace BlazorApp1.Services.OrderFiles;

public class BasketItem
{
    public int Id { get; set; } 
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public int Quantity { get; set; }
    public TicketType Type { get; set; }
    public DateTime? ScreeningDate { get; set; }
    public string MoviePosterUrl { get; set; }
    
    public double Price { get; set; }

}