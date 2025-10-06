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

    // A Method that prints a Menu, prompts and reads an index, then subtracts to match right index in Options Arr and returns corrected index.
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

    // Midware Method that takes in an index parameter. It loads contacts from textfile, in which index triggers referenced action.
    bool MeddlingKid(int i)
    {
        (bool getContacts, contactList) = FileHandler.ReadContacts();
        if (!getContacts) return false;

        if (options[i] != "Close") Console.WriteLine($" \n----- {options[i]} contact:");
        if (i >= 0 && i < options.Length && i != 5)
        {
            switch (i)
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

    // Method to visually simulate the app booting and shutting down.
    void OpenCloseApp(bool open)
    {
        string msg = open ? "Booting...\n" : "\nTurning off...\n";
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        Helpers.WriteSlow(msg, 50);
        Console.ResetColor();
        if (open) Thread.Sleep(150);
    }
}