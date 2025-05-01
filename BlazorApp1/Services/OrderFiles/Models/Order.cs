// Services/Orders/Models/Order.cs
using System;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        // Relacionamentos
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        
        public int ShippingAddressId { get; set; } // Foreign key
        public Adress ShippingAddress { get; set; } // Navigation property
    }
}