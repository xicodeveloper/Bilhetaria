namespace BlazorApp1.Services.DataBase.DBEntities.Builders;

public class AddressBuilder
{
    private string _street;
    private string _city;
    private string _state;
    private string _zipCode;
    private string _country;
    private string _number;
    private Guid _userId;
    private User _user;


    public static AddressBuilder Empty() => new();

    public AddressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public AddressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public AddressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }

    public AddressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public AddressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }

    public AddressBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public AddressBuilder WithUser(User user)
    {
        _user = user;
        _userId = user.Id;
        return this;
    }
    public Adress Build() => new()
    {
        Street = _street,
        City = _city,
        State = _state,
        ZipCode = _zipCode,
        Country = _country,
        Number = _number,
        UserId = _userId,
        User = _user
    };
}