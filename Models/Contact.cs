class Contact
{

    // --- Properties (data fields) ---
    public string FirstName { get; set; } = "";      // First name of the person
    public string LastName { get; set; } = "";       // Last name of the person
    public string Street { get; set; } = "";  // Street + house number
    public string ZipCode { get; set; } = "";     // Postal/ZIP code
    public string City { get; set; } = "";           // City or town
    public string Phone { get; set; } = "";    // Phone number
    public string Email { get; set; } = "";          // Email address    public long ID;
    public long ID { get; private set; } // Unique identifier for the contact

    public Contact(long _num, string _firstname, string _lastname, string _street, string _zipCode, string _city,
    string _phone, string _email) // Constructor
    {
        ID = _num;
        FirstName = _firstname;
        LastName = _lastname;
        Street = _street;
        ZipCode = _zipCode;
        City = _city;
        Phone = _phone;
        Email = _email;
    }

}