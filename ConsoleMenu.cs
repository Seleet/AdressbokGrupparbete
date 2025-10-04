using System;

namespace AddressBookGroupProject
{
    public class ConsoleMenu
    {
        public static void Show(AddressBook addressBook)
        {
            while (true)
            {
                Console.WriteLine("\n=== Address Book ===");
                Console.WriteLine("1. List all contacts");
                Console.WriteLine("2. Search (first name / last name / city)");
                Console.WriteLine("3. Add contact");
                Console.WriteLine("4. Update contact");
                Console.WriteLine("5. Delete contact");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addressBook.ListAll();
                        break;

                    case "2":
                        addressBook.RunSearch();
                        break;

                    case "3":
                        var newContact = ConsoleUI.ReadContactFromConsole();
                        addressBook.AddContact(newContact);
                        break;

                    case "4":
                    {
                        var idx = addressBook.PromptIndexOrCancel();
                        if (idx < 0) break;

                        var current = addressBook.GetByIndex(idx);
                        if (current is null) { Console.WriteLine("Invalid index."); break; }

                        var updated = ConsoleUI.ReadContactFromConsole(current);
                        addressBook.UpdateContact(idx, updated);
                        break;
                    }

                    case "5":
                    {
                        var idx = addressBook.PromptIndexOrCancel("Choose # to delete (or ENTER to cancel): ");
                        if (idx < 0) break;
                        addressBook.DeleteContact(idx);
                        break;
                    }

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
