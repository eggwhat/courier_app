namespace SwiftParcel.ExternalAPI.Baronomat.Application.DTO;

using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;
using System.Text.Json.Serialization;

public class OrderAddressDto
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string surname { get; set; }
    public string country { get; set; }
    public string city { get; set; }
    public string street { get; set; }
    public string homeNumber { get; set; }
    public string apartmentNumber { get; set; }
    public string note { get; set; }
    public string postalCode { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    [JsonConstructor]
    public OrderAddressDto(int id, string firstName, string surname, string country, string city, string street, string homeNumber, string apartmentNumber, string note, string postalCode, string email, string phoneNumber)
    {
        this.id = id;
        this.firstName = firstName;
        this.surname = surname;
        this.country = country;
        this.city = city;
        this.street = street;
        this.homeNumber = homeNumber;
        this.apartmentNumber = apartmentNumber;
        this.note = note;
        this.postalCode = postalCode;
        this.email = email;
        this.phoneNumber = phoneNumber;
    }   

    public OrderAddressDto(AddressDto address, string name, string email)
    {
        id = 0;
        firstName = name.Split(' ')[0];
        if (name.Split(' ').Length > 1)
            surname = name.Split(' ')[1];
        else
            surname = firstName;
        country = address.Country;
        city = address.City;
        street = address.Street;
        homeNumber = address.BuildingNumber;
        apartmentNumber = address.ApartmentNumber;
        note = "Dont throw";
        postalCode = address.ZipCode;
        this.email = email;
        phoneNumber = "123456789";
    }
}