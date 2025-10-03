using ContactManager.Controllers;
using System.Net.Quic;
namespace ContactManager.Models;


class Program
{
    private static ContactController controller = new ContactController(); //Creates the controller
    static void Main()
    {
        bool quit = false;
        controller.Load(); //Loads th saved info from controller

        while (quit = true)
        {
            Console.WriteLine("Welcome to your address book\nPlease select one off the following\n\n1 for add contact\n2 for edit contact\n3 for contact register\n4 for search\n5 for quit");
            string awnser = (Console.ReadLine().ToLower());

            switch (awnser)
            {
                case "5": //Finnishing the program 
                    {
                        Console.WriteLine("Quitting program");
                        System.Environment.Exit(0);
                        break;
                    }
                case "1": //Add a contact to list
                    {
                        Console.WriteLine("Enter first name");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter last name");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter street address");
                        string streetAddress = Console.ReadLine();
                        Console.WriteLine("Enter postal code");
                        string postalCode = Console.ReadLine();
                        Console.WriteLine("Enter city");
                        string city = Console.ReadLine();
                        Console.WriteLine("Enter phonenumber");
                        string phonenumber = Console.ReadLine();
                        Console.WriteLine("Enter email");
                        string emailAddress = Console.ReadLine();



                        controller.Add( firstName,  lastName,  streetAddress,  postalCode,  city, emailAddress,  phonenumber);
                        controller.Save();
                        Success();
                        break;
                             
                    }

                case "2": //Editt the contact list
                    {
                        Console.WriteLine("What contact do you want to edit?");
                        List<Models.Contact> myContactList = SearchForPerson();
                        if (myContactList.Count == 1)
                        {
                            Console.WriteLine("Wich field do you want to edit or delete?\nTo delete enter 1\n\nTo edit field enter\n2 for Firs name\n3 for last name\n4 for street address\n5 for city\n6 for email address\n7 for phone number\n");
                            string myAwnser = Console.ReadLine().ToLower();

                            Contact myContact = myContactList[0];

                            switch (myAwnser)
                            {
                                case ("2"):
                                    {
                                        Console.WriteLine("Enter first name");
                                        myContact.FirstName = Console.ReadLine();
                                        break;
                                    }
                                case ("3"):
                                    {
                                        Console.WriteLine("Enter last name");
                                        myContact.LastName = Console.ReadLine();
                                        break;
                                    }
                                case ("4"):
                                    {
                                        Console.WriteLine("Enter street address");
                                        myContact.StreetAddress = Console.ReadLine();
                                        break;
                                    }
                                case ("5"):
                                    {
                                        Console.WriteLine("Enter city");
                                        myContact.City = Console.ReadLine();
                                        break;
                                    }
                                case ("6"):
                                    {
                                        Console.WriteLine("Enter email address");
                                        myContact.EmailAddress = Console.ReadLine();
                                        break;
                                    }
                                case ("7"):
                                    {
                                        Console.WriteLine("Enter phone number");
                                        myContact.PhoneNumber = Console.ReadLine();
                                        break;
                                    }
                                case "1":
                                    {
                                        controller.Delete(myContact);
                                        Console.WriteLine("Contact deleated");
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Unvalid field");
                                        break;
                                    }
                            }
                            Success();
                        }
                        else if (myContactList.Count > 1) //If there is too many matches
                        {
                            Console.WriteLine("Too many matches, please try again");
                        }
                        else //If there is no matches
                        {
                            Console.WriteLine("You have no matches, please try again");
                        }
                            break;
                    }
                case "3": //See the contact register
                    {
                        foreach (var contact in controller.GetAll())
                            Console.WriteLine(contact);
                        break;
                    }
                case "4": //Serch after person mabye incorperated in the contact list register?
                    {
                        Console.WriteLine("Who are you searching for?");
                        List<Models.Contact> myContactList = SearchForPerson();

                        foreach (var contact in myContactList)
                            Console.WriteLine(contact);


                        break;
                    }
                default: //Exeption
                    {
                        Console.WriteLine("Cannot read command, please try again");
                        break;
                    }
            }

        }

    }

    private static void Success() //Saves info in controller
    {
        Console.WriteLine("Saved");
        controller.Save();
    }

    private static List<Models.Contact> SearchForPerson()
    {
        string searchTerm = Console.ReadLine();
        List<Models.Contact> myContactList = controller.Search(searchTerm);
        return myContactList;
    }
}