using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.Purchase.OrderState;

public class CancelledState: IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public CancelledState(Order order  ,IUnitOfWork unitOfWork) 
    {
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public Task Pay(double price, PaymentMethod method)
    {
        throw new InvalidOperationException("O pedido já foi cancelado. Não é possível pagar.");
    }

    public Task Cancel()
    {
        throw new InvalidOperationException("O pedido já foi cancelado.");
    }
}