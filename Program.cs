using System;

namespace AddressBookGroupProject
{
    class Program
    {
        static void Main()
        {
            var addressBook = new AddressBook("data/contacts.txt"); // Create an AddressBook instance and set file path

            addressBook.Load();                 // Load contacts from file at program start
            ConsoleMenu.Show(addressBook);      // Show the main menu and handle user input
            addressBook.Save();                 // Save contacts to file on exit
        }
    }
}
