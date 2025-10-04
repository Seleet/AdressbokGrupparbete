class AddressBook
{
    string[] options = ["List", "Create", "Update", "Delete", "Find", "Close"];

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
        if (options[num] != "Close") Console.WriteLine($" \n----- {options[num]} contact:");
        if (num >= 0 && num < options.Length && num != 5)
        {
            switch (num)
            {
                case 0: ContactHandlers.ListContacts(); break;
                case 1: ContactHandlers.CreateContact(); break;
                case 2: ContactHandlers.UpdateContact(); break;
                case 3: ContactHandlers.DeleteContact(); break;
                case 4: ContactHandlers.FindContacts(); break;
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
        string msg = open ? "Turning on" : "Turning off";
        Thread.Sleep(200);
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"\n{msg} application");
        Console.ResetColor();
        Thread.Sleep(200);
        Console.WriteLine("...");
        Thread.Sleep(200);
        Console.WriteLine("..");
        Thread.Sleep(200);
        Console.WriteLine(".");
        if (open) Thread.Sleep(200);
    }
}