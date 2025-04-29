
using BlazorApp1.Services;
using BlazorApp1.Services.DataBase;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public User GetById(int id) => _context.Users.Find(id);

    public IEnumerable<User> GetAll() => _context.Users.ToList();

    public void Add(User product) => _context.Users.Add(product);

    public void Remove(User product) => _context.Users.Remove(product);
    
}
