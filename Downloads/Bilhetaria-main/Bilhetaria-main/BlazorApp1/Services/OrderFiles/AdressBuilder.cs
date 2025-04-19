namespace BlazorApp1.Services.OrderFiles;

public class AdressBuilder
{
    private string _street;
    private string _city;
    private string _state;
    private string _zipCode;
    private string _country;
    private int _number;
    public static AdressBuilder Empty() => new ();

    public AdressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }
    public AdressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }
    public AdressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }
    public AdressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }
    public AdressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }
    public AdressBuilder WithNumber(int number)
    {
        _number = number;
        return this;
    }

    public Adress Build() => new()
    {
        Street = _street,
        City = _city,
        State = _state,
        ZipCode = _zipCode,
        Country = _country,
        Number = _number
    };
}

    