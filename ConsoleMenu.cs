namespace AddressBookGroupProject
{
    public class ConsoleMenu
    {
        // Only static to make the app runnable
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
                    case "1": addressBook.ListAll(); break;
                    case "2": addressBook.RunSearch(); break;
                    case "3": addressBook.AddContact(); break;
                    case "4": addressBook.UpdateContact(); break;
                    case "5": addressBook.DeleteContact(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }
    }
}
