// Services/Orders/Models/Order.cs

using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;

namespace BlazorApp1.Services.DataBase.DBEntities
{
    public class Order  : DbItem
    {
        public Guid Id { get; set; }
        
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        [NotMapped]
        public IOrderState State { get; set; }
        
        // Relacionamentos
        public List<BasketItem> Items { get; set; } = new();
        
        public Guid ShippingAddressId { get; set; } // Foreign key
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