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

}