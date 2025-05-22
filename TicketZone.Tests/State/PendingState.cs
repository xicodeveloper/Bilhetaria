using BlazorApp1.Services.DataBase;
using Xunit;
using Moq;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using BlazorApp1.Services.DataBase.OrderState;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.DataBase.Repository;

namespace BlazorApp1.Tests.Services.DataBase.OrderState
{
    public class PendingStateTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Order _order;
        private readonly PendingState _pendingState;

        public PendingStateTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _order = new Order { UserId = Guid.NewGuid(), Status = OrderStatus.Pending };
            _pendingState = new PendingState(_order, _unitOfWorkMock.Object);
        }

        [Fact]
        public void Pay_WithSufficientBalance_UpdatesStatusAndBalance()
        {
            // Arrange
            var wallet = new WalletUser
            {
                CreditCardSaldo = 100,
                MbwaySaldo = 100,
                ApplePaySaldo = 100
            };

            var repoMock = new Mock<IDatabaseRepository<WalletUser>>();
            repoMock.Setup(r => r.GetWithQuery(It.IsAny<Func<IQueryable<WalletUser>, IQueryable<WalletUser>>>()))
                .Returns(new List<WalletUser> { wallet });

            _unitOfWorkMock.Setup(u => u.GetRepository<WalletUser>())
                .Returns(repoMock.Object);

            // Act
            _pendingState.Pay(50, PaymentMethod.CreditCard);

            // Assert
            Assert.Equal(OrderStatus.Completed, _order.Status);
            Assert.Equal(50m, wallet.CreditCardSaldo);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void Pay_WithInsufficientBalance_ThrowsException()
        {
            // Arrange
            var wallet = new WalletUser { CreditCardSaldo = 30 };
            var repoMock = new Mock<IDatabaseRepository<WalletUser>>();
            repoMock.Setup(r => r.GetWithQuery(It.IsAny<Func<IQueryable<WalletUser>, IQueryable<WalletUser>>>()))
                .Returns(new List<WalletUser> { wallet });

            _unitOfWorkMock.Setup(u => u.GetRepository<WalletUser>())
                .Returns(repoMock.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => 
                _pendingState.Pay(50, PaymentMethod.CreditCard));
        }

        [Fact]
        public void Cancel_UpdatesStatusToCancelled()
        {
            // Act
            _pendingState.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, _order.Status);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}