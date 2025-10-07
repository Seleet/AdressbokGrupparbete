class Program
{
    static void Main()
    {

        // ======================================
        // FIX (Martin, 2025-10-07):
        // Issue: App crashed on startup due to hardcoded path "E:\"
        // Solution: Create directory and empty file automatically at launch
        // ======================================


        // Get the user's platform-specific ApplicationData folder (AppData on Windows, Library/Application Support on macOS, .config on Linux)
        // and combine it with the subfolder "AddressBookApp" to create the app's data directory path.
        var appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AddressBookApp");



        Directory.CreateDirectory(appDir);  // Ensure the directory exists (no-op if already present).
        var file = Path.Combine(appDir, "contacts.csv");

        AddressBook addressbook = new(file);
        addressbook.RunAddressBookApp();
    }
}
