using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services.DataBase.OrderState;

public class PendingState : IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public PendingState(Order order,IUnitOfWork unitOfWork)
    {
        
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public void Pay(double price, PaymentMethod method)
    {
        // Procesar pago
        var wallet = _unitOfWork
            .GetRepository<WalletUser>()
            .GetWithQuery(q => q.Include(w => w.User).Where(w => w.User.Id == Order.UserId))
            ?.FirstOrDefault();
        if (wallet == null) throw new Exception("Wallet não encontrada");

        decimal amount = (decimal)price;
        switch (method)
        {
            case PaymentMethod.CreditCard:
                if (wallet.CreditCardSaldo < amount)
                    throw new Exception("Saldo insuficiente no cartão");
                wallet.CreditCardSaldo -= amount;
                break;
            case PaymentMethod.Mbway:
                if (wallet.MbwaySaldo < amount)
                    throw new Exception("Saldo insuficiente em MBWay");
                wallet.MbwaySaldo -= amount;
                break;
            case PaymentMethod.ApplePay:
                if (wallet.ApplePaySaldo < amount)
                    throw new Exception("Saldo insuficiente em Apple Pay");
                wallet.ApplePaySaldo -= amount;
                break;
        }

        // Actualizar estado do pedido
        Order.Status = OrderStatus.Completed;
        _unitOfWork.Commit();
    }

    public void Cancel()
    {
        Order.Status = OrderStatus.Cancelled; 
        _unitOfWork.Commit();
    }
}