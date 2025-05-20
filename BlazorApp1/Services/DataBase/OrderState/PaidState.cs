using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.OrderState;

public class PaidState : IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public PaidState(Order order, IUnitOfWork unitOfWork)
    {
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public void Pay(double price, PaymentMethod method)
    {
        throw new InvalidOperationException("O pedido já foi pago.");
    }

    public void Cancel()
    {
        // Lógica para reembolso 
        Order.Status = OrderStatus.Cancelled;
        _unitOfWork.Commit();
    }
}