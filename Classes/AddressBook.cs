/// <summary>
/// Represents the main AddressBook application.
/// Handles the program flow, main menu, and links user choices to the correct contact operations.
/// </summary>

class AddressBook
{

    private readonly string[] options = ["List", "Create", "Update", "Delete", "Find", "Close"]; //Private read only encapsulated
    private List<Contact> contactList = [];

    public AddressBook(string filePath)
    {
        FileHandler.SetFilePath(filePath);
    }

    public void RunAddressBookApp() //This Is the method that starts the Program
    {

        OpenCloseApp(true); //OpenCloseApp writes Welcoming "booting" with slow threadsleep


        var (ok, list) = FileHandler.ReadContacts(); //Loads one time
        contactList = ok ? list : new List<Contact>();

        bool openAddressBook = true; //Sets a boolean to true to keep The Addressbookwhile loop runninmg.
        while (openAddressBook)
        {
            int choice = MainMenu(); //creates an int choice to recieve the choice made in  MainMenu Method.
            openAddressBook = HandleMenuChoice(choice);
        }
        OpenCloseApp(false);
    }

    // Comment: Main menu loop out 'options array'. User pick an action between [1 - length of array]
    //          PromptIntQuestion handles the verification logic, once a number is confirmed,
    //          to match the index InputNum subtracts -1 and the method returns the index.
    public int MainMenu()
    {
        Console.WriteLine($"\n-- Choose an action by entering a number [1-{options.Length}]:");
        for (int i = 0; i < options.Length; i++) //forloops thru the options array and writes contact unless its "Close app"
        {
            string msg = options[i] != "Close" ? "contact" : "app";
            Console.WriteLine($"{i + 1}. {options[i]} {msg}");
        }

        int index = Helpers.PromptIntQuestion(""); // PromptIntQuestion Validates input is correct with 
        while (index < 1 || index > options.Length) // Added lower-bound check to prevent values below 1 (e.g., 0) from becoming -1 after index--.
        {
            index = Helpers.PromptIntQuestion($"Not a valid number. Enter a number between [1-{options.Length}]:"); //If index is higher than options, try again
        }

        index--; //index reduced by 1 because arrays start with index 0

        return index;
    }

    bool HandleMenuChoice(int num) //Changed name from MeddlingKid for clarity
    {


        (bool getContacts, contactList) = FileHandler.ReadContacts(); // deconstruction of tuple.

        bool isClose = num == options.Length - 1;
        if (num < 0 || num >= options.Length) return false;
        if (isClose) return false;
        Console.WriteLine($"\n----- {options[num]} contact:");


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

    void OpenCloseApp(bool open)
    {
        string msg = open ? "Booting...\n" : "\nTurning off...\n";
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        Helpers.WriteSlow(msg, 50);
        Console.ResetColor();
        if (open) Thread.Sleep(150);
    }
}