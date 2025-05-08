using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.Purchase.OrderState;

public class PaidState : IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public PaidState(Order order, IUnitOfWork unitOfWork)
    {
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public Task Pay(double price, PaymentMethod method)
    {
        throw new InvalidOperationException("O pedido já foi pago.");
    }

    public async Task Cancel()
    {
        // Lógica para reembolso 
        Order.Status = OrderStatus.Cancelled;
        await _unitOfWork.CommitAsync();
    }
}