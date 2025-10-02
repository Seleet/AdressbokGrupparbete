namespace AddressBookGroupProject
{
    public class AddressBook
    {
        private readonly List<Contact> _contacts = new();

        public void ListAll()
        {
            if (_contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\n--- Contact List ---");
            int i = 1;
            foreach (var c in _contacts)
            {
                Console.WriteLine($"{i++}. {c.FirstName} {c.LastName}, {c.City} ({c.PhoneNumber})");
            }
        }


        public void RunSearch()
        {
            Console.WriteLine("[TODO] Search function");
        }


        public void AddContact()
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            string lastName = Console.ReadLine() ?? "";

            Console.Write("Street address: ");
            string street = Console.ReadLine() ?? "";

            Console.Write("Postal code: ");
            string postal = Console.ReadLine() ?? "";

            Console.Write("City: ");
            string city = Console.ReadLine() ?? "";

            Console.Write("Phone number: ");
            string phone = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            var contact = new Contact(firstName, lastName, street, postal, city, phone, email);
            _contacts.Add(contact);

            Console.WriteLine("Contact added!");
            _contacts.Add(contact);

            Console.WriteLine("Contact added!");
        }


        public void UpdateContact()
        {
            Console.WriteLine("[TODO] Update a contact");
        }

        public void DeleteContact()
        {
            Console.WriteLine("[TODO] Delete a contact");
        }

        // --- File IO ---
        public void LoadFromFile(string path)
        {
            Console.WriteLine($"[TODO] Load from file: {path}");
        }

        public void SaveToFile(string path)
        {
            Console.WriteLine($"[TODO] Save to file: {path}");
        }

    }
}
