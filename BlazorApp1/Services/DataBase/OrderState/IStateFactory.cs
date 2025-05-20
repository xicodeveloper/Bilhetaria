using BlazorApp1.Services.DataBase.DBEntities;

namespace BlazorApp1.Services.DataBase.OrderState;

public interface IStateFactory
{
    IOrderState CreateState(Order order); 
}