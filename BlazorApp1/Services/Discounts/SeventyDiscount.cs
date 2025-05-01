namespace BlazorApp1.Services.Discounts;
public class SeventyDiscount : IStrategyDiscount
{
    public double CalculateDiscount(double price)
    {
        return price * 0.3;
    }
}