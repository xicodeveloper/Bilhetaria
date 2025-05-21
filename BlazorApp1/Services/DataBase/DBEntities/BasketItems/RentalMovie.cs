using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.DBEntities.BasketItems;

public class RentalMovie : BasketItem
{
    public DateTime Validade { get; set; } = DateTime.Now.AddDays(14);
    
    public override TicketType GetTicketType()
    {
        return TicketType.Rental;
    }
}