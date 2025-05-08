using BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public interface IOrderState
{
    Order Order { get; set; }
    Task Pay(double price, PaymentMethod method);
    Task Cancel();
}