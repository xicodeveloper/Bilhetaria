namespace BlazorApp1.Services.Models;

public class MovieDetails
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    public string poster_path { get; set; }
    public string Tagline { get; set; }
    public int Runtime { get; set; }
    public double VoteAverage { get; set; }
    public string backdrop_path { get; set; }
    public List<Genre> Genres { get; set; }
}