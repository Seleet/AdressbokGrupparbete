class Program
{

    static void Main()
    {

        // Get the user's platform-specific ApplicationData folder (AppData on Windows, Library/Application Support on macOS, .config on Linux)
        // and combine it with the subfolder "AddressBookApp" to create the app's data directory path.(Martin)
        var appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AddressBookApp");



        Directory.CreateDirectory(appDir);  // Ensure the directory exists (no-op if already present)(Martin).
        var file = Path.Combine(appDir, "contacts.csv"); // Combine the app directory with the filename to get the full file path that gets forwarded to FileRepository via Addressbook constructor

        AddressBook addressbook = new(file); // Main instantiates the AddressBook class.
                                             // Separating responsibilities (startup, logic, data handling) improves maintainability and allows future expansion.

        addressbook.RunAddressBookApp(); //starts the AddressBook application displays the main menu and handles user choices.
    }
}
