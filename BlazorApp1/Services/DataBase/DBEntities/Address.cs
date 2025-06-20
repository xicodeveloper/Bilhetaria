// Services/Orders/Models/Address.cs

using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Services.DataBase.DBEntities;

public class Address  : DbItem
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public bool IstheOne { get; set; } = false;
}