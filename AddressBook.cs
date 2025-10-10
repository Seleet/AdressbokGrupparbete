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

    foreach (var option in options)
    {
        string msg = option != MenuOption.Close ? "contact" : "app";
        Console.WriteLine($"{(int)option}. {option} {msg}");
    }

    int index = Helpers.PromptIntQuestion("");
    while (index < 1 || index > options.Length)
    {
        index = Helpers.PromptIntQuestion($"Not a valid number. Enter a number between [1-{options.Length}]:");
    }

    return index;
}

    bool HandleMenuChoice(int choiceNumber) //Changed name from MeddlingKid for clarity
    {
         MenuOption choice = (MenuOption)choiceNumber;

        switch (choice)
        {
            case MenuOption.List: ContactHandlers.ListContacts(contactList); break;
            case MenuOption.Create: ContactHandlers.CreateContact(contactList); break;
            case MenuOption.Update: ContactHandlers.UpdateContact(contactList); break;
            case MenuOption.Delete: ContactHandlers.DeleteContact(contactList); break;
            case MenuOption.Find: ContactHandlers.FindContacts(contactList); break;
            case MenuOption.Close: return false;
            default: return true; // Automatically return to main menu without asking
        }
        return true;
        //return Helpers.PromptYesNoQuestion("\nReturn to main menu [y/n]? ");

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