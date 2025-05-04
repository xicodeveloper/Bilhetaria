using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public class CanceledState: IOrderState
{
    public Order Order { get; set; }

    public void Pay()
    {
        //Não é possível pagar um pedido cancelado
    }

    public void Cancel()
    {
        //Não é possível cancelar um pedido já cancelado
    }
}