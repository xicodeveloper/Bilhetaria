using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services.DataBase.DBEntities.BasketItems;

public class TicketMovie : BasketItem
{
    public DateTime ViewingDate { get; set; }
    public string Seat { get; set; } = string.Empty;
    
    public override TicketType GetTicketType()
    {
        return TicketType.Ticket;
    }
}