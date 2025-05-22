using Xunit;
using BlazorApp1.Services.Discounts;

namespace BlazorApp1.Tests.Services.Discounts
{
    public class ApplyDiscountStrategyTests
    {
        [Fact]
        public void ApplyDiscount_WithInitialTenDiscount_Applies10PercentDiscount()
        {
            // Arrange
            var strategy = new ApplyDiscoutStrategy(new TenDiscount());
            double price = 100;

            // Act
            double result = strategy.ApplyDiscount(price);

            // Assert
            Assert.Equal(90, result);
        }

        [Fact]
        public void SetDiscountStrategy_ChangeToTwentyFiveDiscount_Applies25PercentDiscount()
        {
            // Arrange
            var initialStrategy = new TenDiscount();
            var newStrategy = new TwentyFiveDiscount();
            var applier = new ApplyDiscoutStrategy(initialStrategy);

            // Act
            applier.SetDiscountStrategy(newStrategy);
            double result = applier.ApplyDiscount(100);

            // Assert
            Assert.Equal(75, result);
        }

        [Fact]
        public void ApplyDiscount_WithWithoutDiscount_ReturnsOriginalPrice()
        {
            // Arrange
            var applier = new ApplyDiscoutStrategy(new WithoutDiscount());
            double price = 100;

            // Act
            double result = applier.ApplyDiscount(price);

            // Assert
            Assert.Equal(100, result);
        }

        [Theory]
        [InlineData(50, 100, 50)]
        [InlineData(30, 100, 70)]
        public void ApplyDiscount_WithGenericDiscount_AppliesCorrectPercentage(double percentage, double price, double expected)
        {
            // Arrange
            var genericDiscount = new GenericDiscount(percentage);
            var applier = new ApplyDiscoutStrategy(genericDiscount);

            // Act
            double result = applier.ApplyDiscount(price);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}