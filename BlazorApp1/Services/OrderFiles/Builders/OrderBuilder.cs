// Services/Orders/Builders/OrderBuilder.cs

using BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services.Purchase.OrderState;

namespace BlazorApp1.Services.Orders.Builders;

public class OrderBuilder
{
    private string _number;
    private int _userId;
    private DateTime _date;
    private Basket _basket;
    private Adress _shippingAddress;
    private OrderStatus _state;
    private IOrderState _orderState;

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
        _orderState = StateFromStatus(status);
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
        ShippingAddress = _shippingAddress,
        State = _orderState,
    };


    private IOrderState StateFromStatus(OrderStatus status)
    {
        
        switch (status)
        {
            case OrderStatus.Completed:
                return new PaidState();
            case OrderStatus.Pending:
                return new PendingState();
            default:
                return new CanceledState();
        }

    }
}