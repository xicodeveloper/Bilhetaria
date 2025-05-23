namespace BlazorApp1.Services.DataBase.DBEntities;

public class MovieGenre(string name) : DbItem
{
    public string Name { get; set; } = name;
}