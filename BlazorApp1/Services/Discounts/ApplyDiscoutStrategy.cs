namespace BlazorApp1.Services.Discounts;

public class ApplyDiscoutStrategy
{
    private IStrategyDiscount _discountStrategy;
    private double _discountValue;

    public ApplyDiscoutStrategy(IStrategyDiscount discountStrategy, double discountValue)
    {
        _discountStrategy = discountStrategy;
        _discountValue = discountValue;
    }
    public double ApplyDiscount(float discount)
    {
        return _discountStrategy.CalculateDiscount(discount);
    }
    public void SetDiscount(IStrategyDiscount discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
}