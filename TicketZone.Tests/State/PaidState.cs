using BlazorApp1.Services.DataBase;
using Xunit;
using Moq;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;

namespace BlazorApp1.Tests.Services.DataBase.OrderState
{
    public class PaidStateTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Order _order;
        private readonly PaidState _paidState;

        public PaidStateTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _order = new Order { Status = OrderStatus.Completed };
            _paidState = new PaidState(_order, _unitOfWorkMock.Object);
        }

        [Fact]
        public void Pay_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => 
                _paidState.Pay(100, PaymentMethod.CreditCard));
        }

        [Fact]
        public void Cancel_UpdatesStatusToCancelled()
        {
            // Act
            _paidState.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, _order.Status);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}