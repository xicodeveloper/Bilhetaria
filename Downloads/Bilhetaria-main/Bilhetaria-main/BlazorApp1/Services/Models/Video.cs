namespace BlazorApp1.Services.Models;
public class Video
{
    public string Key { get; set; }
    public string Type { get; set; }
    public string GetUrl() => $"https://www.youtube.com/watch?v={Key}";
}