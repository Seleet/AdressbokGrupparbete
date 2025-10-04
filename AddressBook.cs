namespace AddressBookGroupProject
{

    public class AddressBook
    {
        private readonly List<Contact> _contacts = new(); // Internal list of contacts
        private readonly string _path;                   // Path to the storage file


        public AddressBook(string path)
        {
            _path = path;
        }

        // ---------- Persistence ----------
        /// <summary>
        /// Load all contacts from the text file.
        /// Creates an empty file if none exists.
        /// </summary>
        public void Load()
        {
            _contacts.Clear();

            var dir = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, string.Empty);
                Console.WriteLine($"[INIT] Created empty file: {Path.GetFullPath(_path)}");
                return;
            }

            using var sr = new StreamReader(_path);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                var c = Contact.FromLine(line);
                if (c != null) _contacts.Add(c);
            }

            Console.WriteLine($"Loaded {_contacts.Count} contact(s) from: {Path.GetFullPath(_path)}");
        }


        // Save all contacts to the text file (overwrite mode "Append=false").

        public void Save()
        {
            var dir = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using var sw = new StreamWriter(_path, false);
            foreach (var c in _contacts) sw.WriteLine(c.ToLine());

            Console.WriteLine($"Saved {_contacts.Count} contact(s) to: {Path.GetFullPath(_path)}");
        }

        // ---------- Queries ----------
        // Print all contacts with full details.

        public void ListAll()
        {
            if (_contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\n--- Contact List ---");
            for (int i = 0; i < _contacts.Count; i++)
            {
                var c = _contacts[i];
                Console.WriteLine($"{i + 1:D3}. Name:    {c.FirstName} {c.LastName}");
                Console.WriteLine($"     Address: {c.StreetAddress}, {c.PostalCode} {c.City}");
                Console.WriteLine($"     Phone:   {c.PhoneNumber}");
                Console.WriteLine($"     Email:   {c.Email}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Search contacts by first name, last name or city.
        /// Prints all matches.
        /// </summary>
        public void RunSearch()
        {
            Console.Write("Search term (first/last/city): ");
            var term = (Console.ReadLine() ?? "").Trim();
            if (term.Length == 0)
            {
                Console.WriteLine("No term entered.");
                return;
            }

            var matches = _contacts
                .Where(c =>
                    c.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    c.LastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    c.City.Contains(term, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
            {
                Console.WriteLine("No matches.");
                return;
            }

            Console.WriteLine($"\n--- {matches.Count} match(es) ---");
            for (int i = 0; i < matches.Count; i++)
                Console.WriteLine($"{i + 1}. {matches[i].FirstName} {matches[i].LastName}, {matches[i].City}");
        }

        // ---------- Commands ----------
        /// <summary>
        /// Add a new contact and save immediately.
        /// </summary>
        public void AddContact(Contact c)
        {
            _contacts.Add(c);
            Save();
            Console.WriteLine("Contact added!");
        }

        /// <summary>
        /// Update an existing contact by index.
        /// </summary>
        public void UpdateContact(int index, Contact updated)
        {
            if (index < 0 || index >= _contacts.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            _contacts[index] = updated;
            Save();
            Console.WriteLine("Contact updated!");
        }

        /// <summary>
        /// Delete a contact by index.
        /// </summary>
        public void DeleteContact(int index)
        {
            if (index < 0 || index >= _contacts.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            var removed = _contacts[index];
            _contacts.RemoveAt(index);
            Save();
            Console.WriteLine($"Deleted: {removed.FirstName} {removed.LastName}");
        }

        // ---------- Helper ----------
        /// <summary>
        /// Show all contacts and let the user pick one by number.
        /// Returns -1 if cancelled or invalid input.
        /// </summary>
        public int PromptIndexOrCancel(string prompt = "Choose # (or ENTER to cancel): ")
        {
            ListAll();
            if (_contacts.Count == 0) return -1;

            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return -1;

            if (int.TryParse(input, out int oneBased) && oneBased >= 1 && oneBased <= _contacts.Count)
                return oneBased - 1;

            Console.WriteLine("Invalid choice.");
            return -1;
        }

        /// <summary>
        /// Get a contact by index or null if out of range.
        /// </summary>
        // Expression-bodied method (shorter syntax for one-line return methods)
        public Contact? GetByIndex(int index)
            => (index >= 0 && index < _contacts.Count) ? _contacts[index] : null;
    }
}
