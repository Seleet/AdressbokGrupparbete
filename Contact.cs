namespace AddressBookGroupProject
{

    public class Contact
    {
        // --- Properties (data fields) ---
        public string FirstName { get; set; } = "";      // First name of the person
        public string LastName { get; set; } = "";       // Last name of the person
        public string StreetAddress { get; set; } = "";  // Street + house number
        public string PostalCode { get; set; } = "";     // Postal/ZIP code
        public string City { get; set; } = "";           // City or town
        public string PhoneNumber { get; set; } = "";    // Phone number
        public string Email { get; set; } = "";          // Email address



        public Contact(string firstName, string lastName, string street, string postal, string city, string phone, string email)
        {
            // Constructor that sets all properties when creating a new Contact
            FirstName = firstName;
            LastName = lastName;
            StreetAddress = street;
            PostalCode = postal;
            City = city;
            PhoneNumber = phone;
            Email = email;
        }


        /// Converts this Contact into a single line of text (pipe-separated).
        /// Example: "Anna|Svensson|Main Street 1|12345|Stockholm|070123456|anna@mail.com"
        public string ToLine()
                   => $"{FirstName}|{LastName}|{StreetAddress}|{PostalCode}|{City}|{PhoneNumber}|{Email}";



        /// Creates a Contact object from a single line of text (pipe-separated).
        /// Returns null if the line does not contain enough fields.
        public static Contact? FromLine(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 7) return null;

            return new Contact(
                parts[0], // FirstName
                parts[1], // LastName
                parts[2], // StreetAddress
                parts[3], // PostalCode
                parts[4], // City
                parts[5], // PhoneNumber
                parts[6]  // Email
            );
        }


    }
}
