using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.OrderState;

public interface IOrderState
{
    Order Order { get; set; }
    Task Pay(double price, PaymentMethod method);
    Task Cancel();
}