using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Services.DataBase.DBEntities;

public abstract class DbItem
{
    [Key]
    public Guid Id { get; set; }
}