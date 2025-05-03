using BlazorApp1.Services.Discounts;

public class ApplyDiscoutStrategy
{
    private IStrategyDiscount _discountStrategy;

    public ApplyDiscoutStrategy(IStrategyDiscount discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    public double ApplyDiscount(double price)
    {
        return _discountStrategy.CalculateDiscount(price);
    }

    public void SetDiscountStrategy(IStrategyDiscount discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
}