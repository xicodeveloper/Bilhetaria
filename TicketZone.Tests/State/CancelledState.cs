using BlazorApp1.Services.DataBase;
using Xunit;
using Moq;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;

namespace BlazorApp1.Tests.Services.DataBase.OrderState
{
    public class CancelledStateTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Order _order;
        private readonly CancelledState _cancelledState;

        public CancelledStateTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _order = new Order { Status = OrderStatus.Cancelled };
            _cancelledState = new CancelledState(_order, _unitOfWorkMock.Object);
        }

        [Fact]
        public void Pay_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => 
                _cancelledState.Pay(100, PaymentMethod.CreditCard));
        }

        [Fact]
        public void Cancel_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => 
                _cancelledState.Cancel());
        }
    }
}