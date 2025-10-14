
/// <summary>
/// Defines all available menu options in the AddressBook application.
/// </summary>
/// <remarks>
/// Using an enum instead of a hardcoded array provides type safety,
/// clearer logic, and automatic synchronization between the code and the menu
/// whenever new options are added.
/// </remarks>
public enum MenuOption
{
    List = 1,
    Create,
    Update,
    Delete,
    Find,
    Close
}
/// <summary>
/// Represents the main AddressBook application.
/// Handles the program flow, main menu, and links user choices to the correct contact operations.
/// </summary>
class AddressBook


{

    private readonly MenuOption[] options = Enum.GetValues<MenuOption>(); // Dynamically retrieves all MenuOption values to keep the menu in sync with the enum definition.

    private List<Contact> contactList = []; // Initializes an empty list using target-typed new for cleaner syntax
    public AddressBook(string filePath) //Constructor that takes a file path as a parameter and sets it in FileRepository.cs
    {
        FileRepository.SetFilePath(filePath);
    }

    /// <summary>
    /// Runs the AddressBook application by displaying the main menu and handling user input.
    /// </summary>
    /// <remarks>
    /// This method keeps running until the user chooses to exit.
    /// </remarks>
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