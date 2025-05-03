using BlazorApp1.Services.Discounts;

public class GenericDiscount : IStrategyDiscount
{
    private readonly double _discountPercentage;

    public GenericDiscount(double discountPercentage)
    {
        _discountPercentage = discountPercentage;
    }

    public double CalculateDiscount(double price)
    {
        return price * (1 - _discountPercentage / 100);
    }
}