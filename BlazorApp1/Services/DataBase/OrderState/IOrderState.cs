using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.OrderState;

public interface IOrderState
{
    Order Order { get; set; }
    void Pay(double price, PaymentMethod method);
    void  Cancel();
}