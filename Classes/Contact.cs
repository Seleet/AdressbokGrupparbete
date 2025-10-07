class Contact
{
    public string Name;
    public string Street;
    public string ZipCode;
    public string City;
    public int Phone;
    public string Email;
    public long ID;

    public Contact(long _num, string _name, string _street, string _zipCode, string _city,
    int _phone, string _email)
    {
        ID = _num;
        Name = _name;
        Street = _street;
        ZipCode = _zipCode;
        City = _city;
        Phone = _phone;
        Email = _email;
    }

    public override string ToString() //ToString() is a method defined in the base class System.Object, which means every class inherits it by default. As a result, every object in C# has a ToString() method. By default, this method returns the fully qualified name of the object's type"
    {
        return $"Contact info -- ID: {this.ID}, Name: {this.Name}, Street: {this.Street}, Zip Code: {this.ZipCode}, City: {this.City}, Phone: {this.Phone}, Email: {this.Email}";
    }

}

