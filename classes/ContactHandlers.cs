class ContactHandlers
{
    static public void ListContacts()
    {
        (bool b, List<Contact> contactList) = FileHandler.ReadContacts();
        if (b)
        {
            LogAllContacts(contactList);
        }
        else
        {
            ShowErrorMsg("read");
        }
    }

    static public void FindContacts()
    {
        string searchTerm = Helpers.PromptStringQuestion("Enter search prhase: ");
        searchTerm = searchTerm.ToLower();
        (bool success, List<Contact> contactList) = FileHandler.ReadContacts();

        if (success)
        {
            List<Contact> foundContact = contactList.Where(
                c => c.Name.Contains(searchTerm) ||
                     c.Street.Contains(searchTerm) ||
                     c.ZipCode.Contains(searchTerm) ||
                     c.City.Contains(searchTerm) ||
                     c.Phone.ToString().Contains(searchTerm) ||
                     c.Email.Contains(searchTerm)
            ).ToList();

            if (foundContact.Count == 0)
            {
                Console.WriteLine("No search results.");
            }
            else
            {
                LogAllContacts(foundContact);
            }

        }
        else
        {
            ShowErrorMsg("read");
        }
    }

    static public void CreateContact()
    {
        (_, List<Contact> contactList) = FileHandler.ReadContacts();

        long ID = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string name = Helpers.PromptStringQuestion("Enter name: ");
        string street = Helpers.PromptStringQuestion("Enter street: ");
        string zipCode = Helpers.PromptStringQuestion("Enter zip code: ");
        string city = Helpers.PromptStringQuestion("Enter city: ");
        int phone = Helpers.PromptIntQuestion("Enter phone: ");
        string email = Helpers.PromptStringQuestion("Enter email: ");

        Contact newContact = new(ID, name.ToLower(), street.ToLower(), zipCode.ToUpper(), city.ToLower(), phone, email.ToLower());

        ContactSummary(newContact);
        bool isCorrect = Helpers.PromptYesNoQuestion("Is this correct [y/n]?");

        if (!isCorrect) newContact = EditField(newContact);

        contactList.Add(newContact);
        LogAllContacts(contactList);
        FileHandler.Write(contactList);

    }

    static public void UpdateContact()
    {
        (int contactIndex, List<Contact> contactList) = GetContactIndex();

        if (contactIndex != 9999)
        {
            ContactSummary(contactList[contactIndex]);
            contactList[contactIndex] = EditField(contactList[contactIndex]);
            Thread.Sleep(300);
            Console.WriteLine("Contact updated!");
            FileHandler.Write(contactList);
        }
    }

    static public void DeleteContact()
    {
        (int contactIndex, List<Contact> contactList) = GetContactIndex();
        if (contactIndex != 9999)
        {
            ContactSummary(contactList[contactIndex]);
            bool isYes = Helpers.PromptYesNoQuestion($"Are you sure you want to delete {contactList[contactIndex].Name} from your contacts [y/n]? ");
            if (isYes)
            {
                bool isDeleted = contactList.Remove(contactList[contactIndex]);
                if (isDeleted)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("Contact removed!");
                }
                FileHandler.Write(contactList);
            }
        }
    }

    // ----------- UTILITY METHODS

    static (int, List<Contact>) GetContactIndex()
    {
        int contactIndex = 9999;
        bool isLocatingContact = true;

        (_, List<Contact> contactList) = FileHandler.ReadContacts();
        foreach (var contact in contactList)
        {
            Thread.Sleep(150);
            Console.WriteLine($"ID: {contact.ID} -- Name: {contact.Name}");
        }


        while (isLocatingContact)
        {
            string contactId = Helpers.PromptStringQuestion("\nEnter ID of the contact you want to update: ");
            if (!contactList.Any(c => c.ID.ToString() == contactId))
            {
                bool isLong = long.TryParse(contactId, out _);
                Console.WriteLine(isLong ? "\nContact not found." : "\nNot a valid ID.");
                isLocatingContact = Helpers.PromptYesNoQuestion("Try again?");
            }
            else
            {
                contactIndex = contactList.FindIndex(c => c.ID.ToString() == contactId);
                isLocatingContact = false;
            }
        }
        return (contactIndex, contactList);
    }

    static Contact EditField(Contact c)
    {
        bool stopLoop = false;
        while (!stopLoop)
        {
            string correctField = Helpers.PromptStringQuestion($"- Which field do you wish to correct? ");

            switch (correctField.ToLower())
            {
                case "name": c.Name = Helpers.PromptStringQuestion("Enter name: "); break;
                case "address": c.Street = Helpers.PromptStringQuestion("Enter street: "); break;
                case "zip code": c.ZipCode = Helpers.PromptStringQuestion("Enter zip code: ").ToUpper(); break;
                case "city": c.City = Helpers.PromptStringQuestion("Enter city: ").ToUpper(); break;
                case "phone": c.Phone = Helpers.PromptIntQuestion("Enter phone: "); break;
                case "email": c.Email = Helpers.PromptStringQuestion("Enter email: "); break;
                default: Console.WriteLine("Invalid option"); break;
            }
            ContactSummary(c);
            stopLoop = Helpers.PromptYesNoQuestion("Is this correct [y/n]? ");
        }
        return c;
    }

    static void ContactSummary(Contact c)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Contact info -- ID: {c.ID}, Name: {c.Name}, Street: {c.Street}, Zip Code: {c.ZipCode}, City: {c.City}, Phone: {c.Phone}, Email: {c.Email}\n");
        Console.ResetColor();
    }

    static void LogAllContacts(List<Contact> contactList)
    {
        foreach (var c in contactList)
        {
            Console.WriteLine($"ID: {c.ID}, Name: {c.Name}, Address: {c.Street}, Zip Code: {c.ZipCode}, City: {c.City}, Phone: {c.Phone}, Email: {c.Email}");
        }
    }

    private static void ShowErrorMsg(string v) // Am I gonna do anything with this or not
    {
        Console.WriteLine($"Couldn't {v} contacts");
    }

}