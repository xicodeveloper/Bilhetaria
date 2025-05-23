using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.BasketItems;
using BlazorApp1.Services.DataBase.DBEntities.Builders;
using BlazorApp1.Services.DataBase.DBEntities.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services.PageServices;

public class AddItemService : IAddItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly NavigationManager _navigation;

    public AddItemService(IUnitOfWork unitOfWork, NavigationManager navigation)
    {
        _unitOfWork = unitOfWork;
        _navigation = navigation;
    }

    public void AddRentalMovie(int movieId, Guid userId, double price, int discount)
    {
        AddMovieToBasket<RentalMovie>(movieId, userId, price * 0.25, discount, "/basket", 
            item => item.Validade = DateTime.Now.AddDays(14));
    }

    public void AddDigitalMovie(int movieId, Guid userId, double price, int discount)
    {
        AddMovieToBasket<DigitalMovie>(movieId, userId, price, discount, "/basket");
    }

    public void AddPhysicalMovie(int movieId, Guid userId, double price, int discount, PhysicalType type)
    {
        AddMovieToBasket<PhysicalMovie>(movieId, userId, price, discount, "/basket",
            item => item.Type = type);
    }

    public void AddTicketMovie(int movieId, Guid userId, double price, int discount, 
                             DateTime viewingDate, string seat, string cinema)
    {
        AddMovieToBasket<TicketMovie>(movieId, userId, price, discount, "/basket",
            item => {
                item.ViewingDate = viewingDate;
                item.Seat = seat;
                item.Cinema = cinema;
            });
    }

    private void AddMovieToBasket<T>(int movieId, Guid userId, double price, int discount, 
                                   string redirectUrl, Action<T> configureItem = null) where T : BasketItem, new()
    {
        try
        {
            if (userId == Guid.Empty) return;

            var user = GetUser(userId);
            if (user?.Addresses == null || user.Addresses.Count == 0)
            {
                Console.WriteLine("User must add an address first");
                _navigation.NavigateTo("/perfilAdmin");
                return;
            }
            
            var existingOrder = GetOrder(userId);
            var movieDb = GetMovie(movieId);

            var newItem = new T()
            {
                MovieId = movieDb.Id,
                Quantity = 1,
                Price = price,
                Discount = discount,
            };

            configureItem?.Invoke(newItem);

            if (existingOrder != null)
            {
                var existingItem = _unitOfWork.GetRepository<T>().GetWithQuery(q => q
                    .Where(i => i.MovieId == movieDb.Id && i.OrderId == existingOrder.Id))
                    .FirstOrDefault();

                if (existingItem != null && existingItem.GetTicketType() != TicketType.Ticket)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    existingOrder.Items.Add(newItem);
                }

                _unitOfWork.GetRepository<Order>().Update(existingOrder);
            }
            else
            {
                var address = GetUsersAddress(user);
                var newOrder = CreateNewOrderWithAddressAndItem(newItem, address);
                _unitOfWork.GetRepository<Order>().Add(newOrder);
            }

            _unitOfWork.Commit();
            _navigation.NavigateTo(redirectUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddMovieToBasket: {ex.Message}");
        }
    }
    
    private User GetUser(Guid userId)
    {
        return _unitOfWork.GetRepository<User>().GetWithQuery(
            q => q
                .Where(u => u.Id == userId)
                .Include(i => i.Addresses)
        ).FirstOrDefault();
    }

    private Order GetOrder(Guid userId)
    {
        return _unitOfWork.GetRepository<Order>().GetWithQuery(
            q => q
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                .Include(o => o.Items)
                .ThenInclude((BasketItem i) => i.Movie)
        ).FirstOrDefault();
    }

    private Movie GetMovie(int movieId)
    {
        return _unitOfWork.GetRepository<Movie>().GetWithQuery(
            q => q.Where(m => m.ApiId == movieId)
        ).FirstOrDefault();
    }

    private Order CreateNewOrderWithAddressAndItem(BasketItem newItem, Address address)
    {
        return new OrderBuilder()
            .WithUserId(address.User.Id)
            .WithDate(DateTime.UtcNow)
            .WithShippingAddress(address)
            .WithItems(new List<BasketItem>())
            .AddItem(newItem)
            .Build();
    }

    private Address GetUsersAddress(User user)
    {
        var shippingAddress = user.Addresses.FirstOrDefault(a => a.IstheOne);
        return shippingAddress;
    }
}