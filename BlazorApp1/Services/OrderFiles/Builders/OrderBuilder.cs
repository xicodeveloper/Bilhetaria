// Services/Orders/Builders/OrderBuilder.cs

using BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Orders.Models;
using K4os.Hash.xxHash;

namespace BlazorApp1.Services.Orders.Builders;

public class OrderBuilder
{
    private string _number;
    private int _userId;
    private DateTime _date;
    private Basket _basket;
    private Adress _shippingAddress;
    private OrderStatus _state;

    public static OrderBuilder Empty() => new();

    public OrderBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public OrderBuilder WithUserId(int userId)
    {
        _userId = userId;
        return this;
    }

    public OrderBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }
    public OrderBuilder WithOrderStatus(OrderStatus status)
    {
        _state = status;
        return this;
    }
    public OrderBuilder WithBasket(Action<BasketBuilder> config)
    {
        var builder = new BasketBuilder();
        config(builder);
        _basket = builder.Build();
        return this;
    }

    public OrderBuilder WithShippingAddress(Action<AddressBuilder> config)
    {
        var builder = new AddressBuilder();
        config(builder);
        _shippingAddress = builder.Build();
        return this;
    }

    public Order Build() => new()
    {
        UserId = _userId,
        Number = _number,
        Date = _date,
        Basket = _basket,
        ShippingAddress = _shippingAddress
    };
}