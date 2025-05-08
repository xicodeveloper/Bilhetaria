// Services/Orders/Models/Order.cs
using System;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.OrderFiles;
using BlazorApp1.Services.Purchase.OrderState;

namespace BlazorApp1.Services.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        [NotMapped]
        public IOrderState State { get; set; }
        
        // Relacionamentos
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        
        public int ShippingAddressId { get; set; } // Foreign key
        public Adress ShippingAddress { get; set; } // Navigation property
        
        public async Task Pay(double price, PaymentMethod method, IStateFactory stateFactory)
        {
            State = stateFactory.CreateState(this);
            await State.Pay(price, method);
        }
        
        public async Task Cancel(StateFactory stateFactory)
        {
            State = stateFactory.CreateState(this);
            await State.Cancel();
        }
        
    }
}