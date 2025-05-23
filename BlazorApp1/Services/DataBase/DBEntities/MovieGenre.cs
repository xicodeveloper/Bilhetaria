namespace BlazorApp1.Services.DataBase.DBEntities;

public class MovieGenre : DbItem
{
    public string Name { get; set; }
    public MovieGenre(string name)
    {
        Name = name;
    }
}