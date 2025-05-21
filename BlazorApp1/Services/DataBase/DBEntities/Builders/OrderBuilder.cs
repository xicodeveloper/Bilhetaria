using BlazorApp1.Services.DataBase.DBEntities.Enum;
    using System.Collections.Generic;
    
    namespace BlazorApp1.Services.DataBase.DBEntities.Builders;
    
    public class OrderBuilder
    {
        private Guid _userId;
        private DateTime _date;
        private List<BasketItem> _items = new();
        private Address _shippingAddress;
        private Guid _shippingAddressId;
        private OrderStatus _status;
    
        public static OrderBuilder Empty() => new();
        
        public OrderBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }
    
        public OrderBuilder WithDate(DateTime date)
        {
            _date = date;
            return this;
        }
    
        public OrderBuilder WithOrderStatus(OrderStatus status)
        {
            _status = status;
            return this;
        }
    
        public OrderBuilder WithItems(List<BasketItem> items)
        {
            _items = items ?? new List<BasketItem>();
            return this;
        }
    
        public OrderBuilder AddItem(BasketItem item)
        {
            _items.Add(item);
            return this;
        }
    
        public OrderBuilder WithShippingAddress(Address address)
        {
            _shippingAddress = address;
            _shippingAddressId = address?.Id ?? Guid.Empty;
            return this;
        }
    
        public OrderBuilder WithShippingAddressId(Guid addressId)
        {
            _shippingAddressId = addressId;
            return this;
        }
    
        public Order Build() => new()
        {
            UserId = _userId,
            Date = _date,
            Status = _status,
            Items = _items,
            ShippingAddress = _shippingAddress,
            ShippingAddressId = _shippingAddressId
        };
    }