namespace BlazorApp1.Services.Discounts;
public class TenDiscount : IStrategyDiscount
{
    public double CalculateDiscount(double price)
    {
        return price * 0.9;
    }
}