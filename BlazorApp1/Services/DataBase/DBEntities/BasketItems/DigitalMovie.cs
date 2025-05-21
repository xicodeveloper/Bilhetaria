using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.DBEntities.BasketItems;

public class DigitalMovie : BasketItem
{
    public override TicketType GetTicketType()
    {
        return TicketType.Digital;
    }
}