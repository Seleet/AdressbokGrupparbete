static class ContactService
{
    public static void ListContacts(List<Contact> contactList) // List all contacts in alphabetical order, Linq Lambda
    {

        contactList = contactList
            .OrderBy(c => c.LastName, StringComparer.OrdinalIgnoreCase)
            .ThenBy(c => c.FirstName, StringComparer.OrdinalIgnoreCase)
            .ToList();


        DisplayAllContacts(contactList); //Then displays them
    }


    static public void FindContacts(List<Contact> contactList) // List all contacts that match search term, Linq Lambda
    {
        string searchTerm = ConsoleHelper.PromptStringQuestion("Enter search phrase: ");
        searchTerm = searchTerm.ToLower();

        List<Contact> foundContact = contactList.Where(
        c => c.FirstName.ToLower().Contains(searchTerm) ||
             c.LastName.ToLower().Contains(searchTerm) ||
             c.Street.ToLower().Contains(searchTerm) ||
             c.ZipCode.ToLower().Contains(searchTerm) ||
             c.City.ToLower().Contains(searchTerm) ||
             c.Phone.ToLower().Contains(searchTerm) ||
             c.Email.ToLower().Contains(searchTerm)
             ).ToList();

        if (foundContact.Count == 0)
        {
            Console.WriteLine("No search results.");
        }
        else
        {
            DisplayAllContacts(foundContact);
        }
    }

    static public void CreateContact(List<Contact> contactList)
    {
        long ID = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string firstname = ConsoleHelper.PromptStringQuestion("Enter firstname: ");
        string lastname = ConsoleHelper.PromptStringQuestion("Enter lastname: ");
        string street = ConsoleHelper.PromptStringQuestion("Enter street: ");
        string zipCode = ConsoleHelper.PromptStringQuestion("Enter zip code: ");
        string city = ConsoleHelper.PromptStringQuestion("Enter city: ");
        string phone = ConsoleHelper.PromptStringQuestion("Enter phone: ");
        string email = ConsoleHelper.PromptStringQuestion("Enter email: ");

        Contact newContact = new(ID, firstname.ToLower(), lastname.ToLower(), street.ToLower(), zipCode.ToUpper(), city.ToLower(), phone.ToLower(), email.ToLower());

        PrintContact(newContact);
        bool isCorrect = ConsoleHelper.PromptYesNoQuestion("Is this correct [y/n]?");

        if (!isCorrect) newContact = EditField(newContact);

        contactList.Add(newContact); // Add new contact to list
        ConfirmAction("Contact created!");
        FileRepository.Write(contactList); // Write updated list to file

    }

    static public void UpdateContact(List<Contact> contactList) // Update existing contact
    {
        bool id = GetContactIndex(contactList, out int contactIndex);

        if (id)
        {
            PrintContact(contactList[contactIndex]);
            contactList[contactIndex] = EditField(contactList[contactIndex]);

            ConfirmAction("Contact updated!");
            FileRepository.Write(contactList);
        }
    }

    static public void DeleteContact(List<Contact> contactList)
    {
        bool id = GetContactIndex(contactList, out int contactIndex);
        if (id)
        {
            PrintContact(contactList[contactIndex]);
            bool isYes = ConsoleHelper.PromptYesNoQuestion($"Are you sure you want to delete {contactList[contactIndex].FirstName} {contactList[contactIndex].LastName} from your contacts [y/n]? ");
            if (isYes)
            {
                bool isDeleted = contactList.Remove(contactList[contactIndex]);
                if (isDeleted)
                {
                    ConfirmAction("Contact removed!");
                }
                FileRepository.Write(contactList);
            }
        }
    }

    // ----------- UTILITY METHODS

    static bool GetContactIndex(List<Contact> contactList, out int contactIndex)
    {
        contactIndex = -1;

        foreach (var contact in contactList)
        {
            Thread.Sleep(150);
            Console.WriteLine($"ID: {contact.ID} -- Name: {contact.FirstName} {contact.LastName}");

        }

        while (true)
        {
            string input = ConsoleHelper.PromptStringQuestion("\nEnter ID of the contact you want to update: ");
            if (long.TryParse(input, out _))
            {
                contactIndex = contactList.FindIndex(c => c.ID.ToString() == input);
                if (contactIndex != -1)
                    return true;

                Console.WriteLine("\nContact not found.");
            }
            else
            {
                Console.WriteLine("\nNot a valid ID.");
            }

            if (!ConsoleHelper.PromptYesNoQuestion("Try again [y/n]? ")) return false;
        }
    }

    static Contact EditField(Contact c)
    {
        bool stopLoop = false;
        while (!stopLoop)
        {
            string correctField = ConsoleHelper.PromptStringQuestion($"- Which field do you wish to correct? ");

            switch (correctField.ToLower())
            {
                case "firstname": c.FirstName = ConsoleHelper.PromptStringQuestion("Enter name: ").ToLower(); break;
                case "lastname": c.LastName = ConsoleHelper.PromptStringQuestion("Enter name: ").ToLower(); break;
                case "address": c.Street = ConsoleHelper.PromptStringQuestion("Enter street: ").ToLower(); break;
                case "zip code": c.ZipCode = ConsoleHelper.PromptStringQuestion("Enter zip code: ").ToUpper(); break;
                case "city": c.City = ConsoleHelper.PromptStringQuestion("Enter city: ").ToLower(); break;
                case "phone": c.Phone = ConsoleHelper.PromptStringQuestion("Enter phone: "); break;
                case "email": c.Email = ConsoleHelper.PromptStringQuestion("Enter email: ").ToLower(); break;
                default: Console.WriteLine("Invalid option"); break;
            }
            PrintContact(c);
            stopLoop = ConsoleHelper.PromptYesNoQuestion("Is this correct [y/n]? ");
        }
        return c;
    }

    static void PrintContact(Contact c) // Formatted display of a contact, capitalizes first letter of names and street, yellow color
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n──────────────────────────────────────────────");
        Console.WriteLine($"ID:       {c.ID}");
        Console.WriteLine($"Name:     {Capitalize(c.FirstName)} {Capitalize(c.LastName)}");
        Console.WriteLine($"Address:  {Capitalize(c.Street)}, {c.ZipCode} {Capitalize(c.City)}");
        Console.WriteLine($"Phone:    {c.Phone}");
        Console.WriteLine($"Email:    {c.Email}");
        Console.WriteLine("──────────────────────────────────────────────");
        Console.ResetColor();
    }

    static string Capitalize(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return s;
        return char.ToUpper(s[0]) + s[1..];
    }

    static void DisplayAllContacts(List<Contact> contactList) // Displays all contacts in the list
    {
        foreach (var c in contactList)
        {
            PrintContact(c);
        }
    }

    static void ConfirmAction(string txt)
    {
        Thread.Sleep(300);
        Console.WriteLine(txt);
    }

}