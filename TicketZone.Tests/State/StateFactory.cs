using BlazorApp1.Services.DataBase;
using Xunit;
using Moq;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;

namespace BlazorApp1.Tests.Services.DataBase.OrderState
{
    public class StateFactoryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly StateFactory _stateFactory;

        public StateFactoryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _stateFactory = new StateFactory(_unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData(OrderStatus.Pending, typeof(PendingState))]
        [InlineData(OrderStatus.Completed, typeof(PaidState))]
        [InlineData(OrderStatus.Cancelled, typeof(CancelledState))]
        public void CreateState_ReturnsCorrectStateType(OrderStatus status, Type expectedType)
        {
            // Arrange
            var order = new Order { Status = status };

            // Act
            var result = _stateFactory.CreateState(order);

            // Assert
            Assert.IsType(expectedType, result);
        }

        [Fact]
        public void CreateState_InvalidStatus_ThrowsException()
        {
            var order = new Order { Status = (OrderStatus)999 };
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                _stateFactory.CreateState(order));
        }
    }
}