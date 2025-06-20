// Services/Orders/Models/Order.cs

using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;

namespace BlazorApp1.Services.DataBase.DBEntities
{
    public class Order  : DbItem
    {
        
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        
        [NotMapped]
        public IOrderState State { get; set; }
        
        // Relacionamentos
        public List<BasketItem> Items { get; set; } = new();
        
        public Guid ShippingAddressId { get; set; } // Foreign key
        public Address ShippingAddress { get; set; } // Navigation property
        
        public void Pay(double price, PaymentMethod method, IStateFactory stateFactory)
        {
            State = stateFactory.CreateState(this);
            State.Pay(price, method);
        }
        
        public void Cancel(StateFactory stateFactory)
        {
            State = stateFactory.CreateState(this);
            State.Cancel();
        }
        
    }
}