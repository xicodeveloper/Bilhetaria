namespace BlazorApp1.Services.Discounts
{
    public interface IStrategyDiscount
    {
        double CalculateDiscount(double totalPrice);
    }
}
