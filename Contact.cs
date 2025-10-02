namespace AddressBookGroupProject
{
    public class Contact
    {
        public string FirstName { get; set; } = "";   // = "" default empty string
        public string LastName { get; set; } = "";
        public string StreetAddress { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string City { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";


        public Contact(string firstName, string lastName, string street, string postal, string city, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetAddress = street;
            PostalCode = postal;
            City = city;
            PhoneNumber = phone;
            Email = email;
        }
    }
}