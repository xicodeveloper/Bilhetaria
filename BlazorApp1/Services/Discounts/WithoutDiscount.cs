namespace BlazorApp1.Services.Discounts;
public class WithoutDiscount : IStrategyDiscount
{
    public double CalculateDiscount(double price)
    {
        return price;
    }
}

