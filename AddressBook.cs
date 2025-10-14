/// <summary>
/// Represents the main AddressBook application.
/// Handles the program flow, main menu, and links user choices to the correct contact operations.
/// </summary>
public enum MenuOption
{
    List = 1,
    Create,
    Update,
    Delete,
    Find,
    Close
}

class AddressBook
{

    private readonly MenuOption[] options = Enum.GetValues<MenuOption>(); //Private read only encapsulated
    private List<Contact> contactList = [];

    public AddressBook(string filePath)
    {
        FileRepository.SetFilePath(filePath);
    }

    public void RunAddressBookApp() //This Is the method that starts the Program
    {

        OpenCloseApp(true); //OpenCloseApp writes Welcoming "booting" with slow threadsleep


        var (ok, list) = FileRepository.ReadContacts(); //Tries to read the file and returns a tuple with a boolean and a list of contacts.
        contactList = ok ? list : new List<Contact>(); //If it fails, it creates an empty list of contacts.

        bool openAddressBook = true; //Sets a boolean to true to keep The Addressbookwhile loop runninmg.
        while (openAddressBook) //Keeps the app running until user decides to close it.
        {
            int choice = MainMenu(); //Starts the main menu and gets user choice.
            openAddressBook = HandleMenuChoice(choice); //Handles the user choice and returns false if user chose to close the app.
        }
        OpenCloseApp(false);
    }


    public int MainMenu() //Displays the main menu and gets user choice.
    {
        Console.WriteLine($"\n-- Choose an action by entering a number [1-{options.Length}]:"); //Dynamically displays the number of options available.
        foreach (var option in options)
        {
            string msg = option switch // "switch expression" to get correct grammar for each menu option.
            {
                MenuOption.Close => "app", // Special case for better UX
                MenuOption.List => "contacts",   // Plural for better UX
                _ => "contact" // Default case
            };

            Console.WriteLine($"{(int)option}. {option} {msg}");
        }


        int index = ConsoleHelper.PromptIntQuestion(""); //Gets user input and verifies that it is a valid integer.
        while (index < 1 || index > options.Length)
        {
            index = ConsoleHelper.PromptIntQuestion($"Not a valid number. Enter a number between [1-{options.Length}]:");
        }

        return index;
    }

    bool HandleMenuChoice(int choiceNumber) //Handles the user choice and calls the appropriate method from ContactHandlers.cs
    {
        MenuOption choice = (MenuOption)choiceNumber;

        switch (choice)
        {
            case MenuOption.List: ContactService.ListContacts(contactList); break;
            case MenuOption.Create: ContactService.CreateContact(contactList); break;
            case MenuOption.Update: ContactService.UpdateContact(contactList); break;
            case MenuOption.Delete: ContactService.DeleteContact(contactList); break;
            case MenuOption.Find: ContactService.FindContacts(contactList); break;
            case MenuOption.Close: return false;
            default: return true; // Automatically return to main menu without asking
        }
        return true;
        //return Helpers.PromptYesNoQuestion("\nReturn to main menu [y/n]? ");

    }

    void OpenCloseApp(bool open) //Writes Welcoming "booting" with slow threadsleep and "turning off" when closing the app.
    {
        string msg = open ? "Booting...\n" : "\nTurning off...\n";
        Console.ForegroundColor = open ? ConsoleColor.Green : ConsoleColor.Yellow;
        ConsoleHelper.WriteSlow(msg, 50);
        Console.ResetColor();
        if (open) Thread.Sleep(150);
    }
}