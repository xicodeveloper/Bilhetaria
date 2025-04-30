namespace BlazorApp1.Services.OrderFiles;

public class BasketBuilder
{
    private readonly List<BasketItem> _items = new();

    public static BasketBuilder Empty() => new();


    public BasketBuilder WithPhysicalTicket(int movieId, string title, string posterUrl, int quantity, DateTime screeningDate, double price)
    {
        _items.Add(new BasketItem
        {
            MovieId = movieId,
            MovieTitle = title,
            MoviePosterUrl = posterUrl,
            Quantity = quantity,
            ScreeningDate = screeningDate,
            Price = price,
            Type = TicketType.Physical
        });
        return this;
    }
    public BasketBuilder WithDigitalTicket(int movieId, string title, string posterUrl, int quantity, DateTime screeningDate, double price)
    {
        _items.Add(new BasketItem
        {
            MovieId = movieId,
            MovieTitle = title,
            MoviePosterUrl = posterUrl,
            Quantity = quantity,
            ScreeningDate = screeningDate,
            Price = price,
            Type = TicketType.Digital
        });
        return this;
    }

    public Basket Build() => new Basket { Items = _items };
}

