using System.Xml;
using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.OrderFiles;



namespace BlazorApp1.Services.Purchase.OrderState;

public class PendingState : IOrderState
{
    private readonly IUnitOfWork _unitOfWork;
    public Order Order { get; set; }

    public PendingState(Order order,IUnitOfWork unitOfWork)
    {
        
        Order = order;
        _unitOfWork = unitOfWork;
    }

    public async Task Pay(double price, PaymentMethod method)
    {
        // Procesar pago
        var wallet = await _unitOfWork.WalletUsers.GetByUserIdAsync(Order.UserId);
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
        await _unitOfWork.CommitAsync();
    }

    public async Task Cancel()
    {
        Order.Status = OrderStatus.Cancelled;
        await _unitOfWork.CommitAsync();
    }
}