namespace AddressBookGroupProject
{
    public class AddressBook
    {
        private readonly List<Contact> _contacts = new();

        // --- Query ---
        public void ListAll()
        {
            Console.WriteLine("[TODO] List all contacts");
        }

        public void RunSearch()
        {
            Console.WriteLine("[TODO] Search function");
        }

        // --- Commands ---
        public void AddContact()
        {
            Console.WriteLine("[TODO] Add a contact");
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
