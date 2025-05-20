using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.OrderState;

public class StateFactory : IStateFactory
{
    private readonly IUnitOfWork _unitOfWork;

    public StateFactory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IOrderState CreateState(Order order)
    {
        return order.Status switch
        {
            OrderStatus.Pending => new PendingState(order, _unitOfWork), // Order en constructor
            OrderStatus.Completed => new PaidState(order, _unitOfWork),
            OrderStatus.Cancelled => new CancelledState(order, _unitOfWork),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}