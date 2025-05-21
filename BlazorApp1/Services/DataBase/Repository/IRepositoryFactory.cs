using BlazorApp1.Services.DataBase.DBEntities;

namespace BlazorApp1.Services.DataBase.Repository;

public interface IRepositoryFactory
{
    IDatabaseRepository<TItem> CreateRepository<TItem>() where TItem : DbItem;
}