namespace BlazorApp1.Services.OrderFiles;

public class OrderBuilder
{
    private int _number;
    private int _id;
    private DateTime _date;
    private Basket _basket;
    private readonly AdressBuilder _addressBuilder = AdressBuilder.Empty();


    private OrderBuilder() { }

    public static OrderBuilder Empty() => new();

    public OrderBuilder WithNumber(int number)
    {
        _number = number;
        return this;
    }
    public OrderBuilder WithId(int number)
    {
        _id = number;
        return this;
    }

    public OrderBuilder WithCreatedOn(DateTime date)
    {
        _date = date;
        return this;
    }

    public OrderBuilder WithShippingAddress(Action<AdressBuilder> action)
    {
        action(_addressBuilder);
        return this;
    }
    public OrderBuilder WithBasket(Action<BasketBuilder> config)
    {
        var builder = new BasketBuilder();
        config(builder);
        _basket = builder.Build();
        return this;
    }

    public Order Build() => new()
    {
        UserId = _id,
        Number = _number,
        Date = _date,
        Basket = _basket,
        ShippingAddress = _addressBuilder.Build()
    };
}