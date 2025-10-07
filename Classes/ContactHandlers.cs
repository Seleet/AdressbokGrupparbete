static class ContactHandlers
{
    static public void ListContacts(List<Contact> contactList)
    {
        LogAllContacts(contactList);
    }

    static public void FindContacts(List<Contact> contactList)
    {
        string searchTerm = Helpers.PromptStringQuestion("Enter search phrase: ");
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
            LogAllContacts(foundContact);
        }
    }

    static public void CreateContact(List<Contact> contactList)
    {
        long ID = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string firstname = Helpers.PromptStringQuestion("Enter firstname: ");
        string lastname = Helpers.PromptStringQuestion("Enter lastname: ");
        string street = Helpers.PromptStringQuestion("Enter street: ");
        string zipCode = Helpers.PromptStringQuestion("Enter zip code: ");
        string city = Helpers.PromptStringQuestion("Enter city: ");
        string phone = Helpers.PromptStringQuestion("Enter phone: ");
        string email = Helpers.PromptStringQuestion("Enter email: ");

        Contact newContact = new(ID, firstname.ToLower(), lastname.ToLower(), street.ToLower(), zipCode.ToUpper(), city.ToLower(), phone.ToString(), email.ToLower());

        ContactSummary(newContact);
        bool isCorrect = Helpers.PromptYesNoQuestion("Is this correct [y/n]?");

        if (!isCorrect) newContact = EditField(newContact);

        contactList.Add(newContact);
        ConfirmAction("Contact created!");
        FileHandler.Write(contactList);

    }

    static public void UpdateContact(List<Contact> contactList)
    {
        bool id = GetContactIndex(contactList, out int contactIndex);

        if (id)
        {
            ContactSummary(contactList[contactIndex]);
            contactList[contactIndex] = EditField(contactList[contactIndex]);

            ConfirmAction("Contact updated!");
            FileHandler.Write(contactList);
        }
    }

    static public void DeleteContact(List<Contact> contactList)
    {
        bool id = GetContactIndex(contactList, out int contactIndex);
        if (id)
        {
            ContactSummary(contactList[contactIndex]);
            bool isYes = Helpers.PromptYesNoQuestion($"Are you sure you want to delete {contactList[contactIndex].FirstName} {contactList[contactIndex].LastName} from your contacts [y/n]? ");
            if (isYes)
            {
                bool isDeleted = contactList.Remove(contactList[contactIndex]);
                if (isDeleted)
                {
                    ConfirmAction("Contact removed!");
                }
                FileHandler.Write(contactList);
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
            string input = Helpers.PromptStringQuestion("\nEnter ID of the contact you want to update: ");
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

            if (!Helpers.PromptYesNoQuestion("Try again [y/n]? ")) return false;
        }
    }

    static Contact EditField(Contact c)
    {
        bool stopLoop = false;
        while (!stopLoop)
        {
            string correctField = Helpers.PromptStringQuestion($"- Which field do you wish to correct? ");

            switch (correctField.ToLower())
            {
                case "firstname": c.FirstName = Helpers.PromptStringQuestion("Enter name: ").ToLower(); break;
                case "lastname": c.LastName = Helpers.PromptStringQuestion("Enter name: ").ToLower(); break;
                case "address": c.Street = Helpers.PromptStringQuestion("Enter street: ").ToLower(); break;
                case "zip code": c.ZipCode = Helpers.PromptStringQuestion("Enter zip code: ").ToUpper(); break;
                case "city": c.City = Helpers.PromptStringQuestion("Enter city: ").ToLower(); break;
                case "phone": c.Phone = Helpers.PromptStringQuestion("Enter phone: "); break;
                case "email": c.Email = Helpers.PromptStringQuestion("Enter email: ").ToLower(); break;
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
        Console.WriteLine($"Contact info -- ID: {c.ID}, First Name: {c.FirstName}, Last Name: {c.LastName}, Street: {c.Street}, Zip Code: {c.ZipCode}, City: {c.City}, Phone: {c.Phone}, Email: {c.Email}");
        Console.ResetColor();
    }
    static void LogAllContacts(List<Contact> contactList)
    {
        foreach (var c in contactList)
        {
            ContactSummary(c);
        }
    }

    static void ConfirmAction(string txt)
    {
        Thread.Sleep(300);
        Console.WriteLine(txt);
    }

}