namespace BlazorApp1.Services.DataBase.DBEntities;

public class Movie : DbItem
{
    public int ApiId { get; set; }
    public string MovieTitle { get; set; }
    public string MoviePosterUrl { get; set; }
    
    public List<MovieGenre> Genres { get; set; }
}