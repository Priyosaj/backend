namespace Priyosaj.Contacts.Entities.OrderEntities;

public class ShippingAddress
{
    public ShippingAddress(string firstName, string lastName, string houseNo, string street, string city, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        HouseNo = houseNo;
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HouseNo { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}