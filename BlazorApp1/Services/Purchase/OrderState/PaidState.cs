using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public class PaidState : IOrderState
{
    public Order Order { get; set; }

    public void Pay()
    {
        //Ordem já etá paga, não pode ser paga novamente
        
    }

    public void Cancel()
    {
        //Cancela pedido
        //Devolve o dinheiro ao cliente
        // guarda as alterações na base de dados
        
    }
}