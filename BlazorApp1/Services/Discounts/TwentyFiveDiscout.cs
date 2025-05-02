namespace BlazorApp1.Services.Discounts;
public class TwentyFiveDiscount : IStrategyDiscount
{
    public double CalculateDiscount(double price)
    {
        return price * 0.75;
    }
}