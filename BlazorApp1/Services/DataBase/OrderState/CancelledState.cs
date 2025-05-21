using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.OrderState;

public class CancelledState: IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public CancelledState(Order order  ,IUnitOfWork unitOfWork) 
    {
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public void Pay(double price, PaymentMethod method)
    {
        throw new InvalidOperationException("O pedido já foi cancelado. Não é possível pagar.");
    }

    public void Cancel()
    {
        throw new InvalidOperationException("O pedido já foi cancelado.");
    }
}