using BlazorApp1.Services.DataBase.DBEntities.Enum;

namespace BlazorApp1.Services;

public interface IAddItemService
{
    void AddRentalMovie(int movieId, Guid userId, double price, int discount);
    void AddDigitalMovie(int movieId, Guid userId, double price, int discount);
    void AddPhysicalMovie(int movieId, Guid userId, double price, int discount, PhysicalType type);
    void AddTicketMovie(int movieId, Guid userId, double price, int discount, DateTime viewingDate, string seat, string cinema);
}