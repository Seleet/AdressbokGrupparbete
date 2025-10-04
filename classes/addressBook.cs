class AddressBook
{
    string[] options = ["List", "Create", "Update", "Delete", "Find", "Close"];

    public void InitAddressBookApp()
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
    public int MainMenu()
    {
        Console.WriteLine($"\n-- Choose an action by entering a number [1-{options.Length}]:");
        for (int i = 0; i < options.Length; i++)
        {
            string msg = options[i] != "Close" ? "contact" : "app";
            Console.WriteLine($"{i + 1}. {options[i]} {msg}");
        }

        int inputNum = Helpers.PromptIntQuestion("");
        while (inputNum > options.Length)
        {
            Console.WriteLine($"Not a valid number. Enter a number between [1-{options.Length}]:");
            _ = int.TryParse(Console.ReadLine(), out inputNum);
        }

        inputNum = inputNum - 1;

        return inputNum;
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

    static void OpenCloseApp(bool open)
    {
        string msg = open ? "Opening" : "Closing";
        Thread.Sleep(200);
        Console.WriteLine(".");
        Thread.Sleep(200);
        Console.WriteLine("..");
        Thread.Sleep(200);
        Console.WriteLine("...");
        Thread.Sleep(200);
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"{msg} application");
        Console.ResetColor();
        if (open) Thread.Sleep(200);
    }
}