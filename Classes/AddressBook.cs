/// <summary>
/// Represents the main AddressBook application.
/// Handles the program flow, main menu, and links user choices to the correct contact operations.
/// </summary>

class AddressBook
{

    string[] options = ["List", "Create", "Update", "Delete", "Find", "Close"];
    List<Contact> contactList = [];

    public AddressBook(string filePath)
    {
        FileHandler.SetFilePath(filePath);
    }

    public void RunAddressBookApp()
    {
        bool openAddressBook = true;
        OpenCloseApp(true);

        while (openAddressBook)
        {
            int choice = MainMenu();
            openAddressBook = MeddlingKid(choice);
        }
        OpenCloseApp(false);
    }

    // Comment: Main menu loop out 'options array'. User pick an action between [1 - length of array]
    //          PromptIntQuestion handles the verification logic, once a number is confirmed,
    //          to match the index InputNum subtracts -1 and the method returns the index.
    public int MainMenu()
    {
        Console.WriteLine($"\n-- Choose an action by entering a number [1-{options.Length}]:");
        for (int i = 0; i < options.Length; i++)
        {
            string msg = options[i] != "Close" ? "contact" : "app";
            Console.WriteLine($"{i + 1}. {options[i]} {msg}");
        }

        int index = Helpers.PromptIntQuestion("");
        while (index > options.Length)
        {
            index = Helpers.PromptIntQuestion($"Not a valid number. Enter a number between [1-{options.Length}]:");
        }

        index--;

        return index;
    }

    bool MeddlingKid(int num)
    {
        // NOTE (Martin, 2025-10-07):
        // File creation is now handled in Program.cs, so this block is unlikely to run.
        // Left in place as a safeguard.

        (bool getContacts, contactList) = FileHandler.ReadContacts();
        if (!getContacts)
        {
            Console.WriteLine("Couldn't read contacts- start with an empty list.");
            contactList = new List<Contact>();
        }

        // Display section title for chosen menu action
        if (options[num] != "Close") Console.WriteLine($" \n----- {options[num]} contact:");
        if (num >= 0 && num < options.Length && num != 5)
        {
            switch (num)
            {
                case 0: ContactHandlers.ListContacts(contactList); break;
                case 1: ContactHandlers.CreateContact(contactList); break;
                case 2: ContactHandlers.UpdateContact(contactList); break;
                case 3: ContactHandlers.DeleteContact(contactList); break;
                case 4: ContactHandlers.FindContacts(contactList); break;
            }
            return Helpers.PromptYesNoQuestion("\nReturn to main menu [y/n]? ");
        }
        else
        {
            return false;
        }
    }

    void OpenCloseApp(bool open)
    {
        string msg = open ? "Booting...\n" : "\nTurning off...\n";
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        Helpers.WriteSlow(msg, 50);
        Console.ResetColor();
        if (open) Thread.Sleep(150);
    }
}