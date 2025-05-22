using Xunit;
using BlazorApp1.Services.Discounts;

namespace BlazorApp1.Tests.Services.Discounts
{
    public class TenDiscountTests
    {
        [Fact]
        public void CalculateDiscount_Price100_Returns90()
        {
            // Arrange
            var discount = new TenDiscount();
            double price = 100;

            // Act
            double result = discount.CalculateDiscount(price);

            // Assert
            Assert.Equal(90, result);
        }

        [Theory]
        [InlineData(200, 180)]
        [InlineData(50, 45)]
        public void CalculateDiscount_VariousPrices_ReturnsCorrectDiscount(double price, double expected)
        {
            var discount = new TenDiscount();
            double result = discount.CalculateDiscount(price);
            Assert.Equal(expected, result);
        }
    }

    public class TwentyFiveDiscountTests
    {
        [Fact]
        public void CalculateDiscount_Price100_Returns75()
        {
            var discount = new TwentyFiveDiscount();
            double result = discount.CalculateDiscount(100);
            Assert.Equal(75, result);
        }
    }

    public class SeventyDiscountTests
    {
        [Fact]
        public void CalculateDiscount_Price100_Returns30()
        {
            var discount = new SeventyDiscount();
            double result = discount.CalculateDiscount(100);
            Assert.Equal(30, result);
        }
    }

    public class WithoutDiscountTests
    {
        [Fact]
        public void CalculateDiscount_Price100_Returns100()
        {
            var discount = new WithoutDiscount();
            double result = discount.CalculateDiscount(100);
            Assert.Equal(100, result);
        }

        [Fact]
        public void CalculateDiscount_ZeroPrice_ReturnsZero()
        {
            var discount = new WithoutDiscount();
            double result = discount.CalculateDiscount(0);
            Assert.Equal(0, result);
        }
    }

    public class GenericDiscountTests
    {
        [Theory]
        [InlineData(50, 100, 50)]
        [InlineData(15, 100, 85)]
        [InlineData(0, 100, 100)]
        [InlineData(100, 100, 0)]
        public void CalculateDiscount_VariousPercentages_ReturnsCorrectDiscount(double percentage, double price, double expected)
        {
            var discount = new GenericDiscount(percentage);
            double result = discount.CalculateDiscount(price);
            Assert.Equal(expected, result);
        }
    }
}