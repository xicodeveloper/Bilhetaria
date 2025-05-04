using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public interface IOrderState
{
    Order Order { get; set; }
    void Pay();
    void Cancel();
}