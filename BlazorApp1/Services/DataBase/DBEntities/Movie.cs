namespace BlazorApp1.Services.DataBase.DBEntities;

public class Movie : DbItem
{
    public string MovieTitle { get; set; }
    public string MoviePosterUrl { get; set; }
}