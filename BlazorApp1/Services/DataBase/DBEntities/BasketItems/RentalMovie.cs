namespace BlazorApp1.Services.DataBase.DBEntities.BasketItems;

public class RentalMovie : BasketItem
{
    public DateTime validade = DateTime.Now.AddDays(14);
}