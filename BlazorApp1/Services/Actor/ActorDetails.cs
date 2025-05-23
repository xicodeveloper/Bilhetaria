namespace BlazorApp1.Services.Actor;

public class ActorDetails
{
    public string Name { get; set; }
    public DateTime? Birthday { get; set; }
    public string PlaceOfBirth { get; set; }
    public string Biography { get; set; }
    public string ProfilePath { get; set; }
    public MovieSummary[] KnownFor { get; set; }
}
