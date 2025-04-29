using BlazorApp1.Services;

public interface IUserRepository
{
    User GetById(int id);
    IEnumerable<User> GetAll();
    void Add(User user);
    void Remove(User user);
}