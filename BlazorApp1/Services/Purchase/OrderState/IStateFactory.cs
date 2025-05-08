using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public interface IStateFactory
{
    IOrderState CreateState(Order order); 
}