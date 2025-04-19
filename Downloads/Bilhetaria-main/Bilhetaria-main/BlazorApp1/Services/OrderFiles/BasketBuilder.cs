namespace BlazorApp1.Services.OrderFiles;

public class BasketBuilder
{
    private readonly List<BasketItem> _items = new();

    public static BasketBuilder Empty() => new();

    public BasketBuilder WithPhysicalTicket(int movieId, string title, int quantity, DateTime screeningDate)
    {
        _items.Add(new BasketItem
        {
            MovieId = movieId,
            MovieTitle = title,
            Quantity = quantity,
            Type = TicketType.Physical,
            ScreeningDate = screeningDate
        });
        return this;
    }
    public BasketBuilder WithDigitalTicket(int movieId, string title, int quantity, DateTime screeningDate)
    {
        _items.Add(new BasketItem
        {
            MovieId = movieId,
            MovieTitle = title,
            Quantity = quantity,
            Type = TicketType.Digital,
            ScreeningDate = screeningDate
        });
        return this;
    }
    public Basket Build() => new Basket { Items = _items };
}

