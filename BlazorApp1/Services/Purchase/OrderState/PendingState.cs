using System.Xml;
using BlazorApp1.Services.Orders.Models;

namespace BlazorApp1.Services.Purchase.OrderState;

public class PendingState : IOrderState
{
    public Order Order { get; set; }

    public void Pay()
    {
        //Retira o dinheiro do Cliente
        //Muda o estado do pedido para pago
        //Cria um novo basket para o cliente
        //Guarda as alterações na base de dados
        
    }

    public void Cancel()
    {
        //Cancela pedido
        //Muda o estado do pedido para cancelado
        //Cria um novo basket para o cliente
        //Guarda as alterações na base de dados
        
    }
}