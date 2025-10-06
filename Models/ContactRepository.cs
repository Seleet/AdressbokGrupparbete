using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ContactManager.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactManager
{
    public class ContactRepository //Handles storing, retrieving, and managing contacts, including saving to and loading from a file
    {
        private List<Models.Contact> contacts = new List<Models.Contact>(); // List to store all contacts in memory
        private string filePath = "Contacts.txt"; // Path to the file where contacts are saved

        public List<Models.Contact> GetAll() => contacts; // Returns the full list of contacts

        public void Add(Models.Contact contact) => contacts.Add(contact); // Adds a new contact to the contacts list

        public void Delete(string name) =>
            contacts.RemoveAll(a => a.FirstName.Equals(name, System.StringComparison.OrdinalIgnoreCase));  // Removes all contacts with the given first name, ignoring case letters.

        public List<Models.Contact> Search(string keyword) => // Returns all contacts whose first or last name contains the given keyword, ignoring case
            contacts.Where(a => a.FirstName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase) ||
                               a.LastName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)).ToList(); //The return value from the search is an

        public void SaveToFile() // Saves all contacts to a file, with each property separate
        {
            File.WriteAllLines(filePath, contacts.Select(a => $"{a.FirstName}|{a.LastName}|{a.StreetAddress}|{a.PostalCode}|{a.City}|{a.PhoneNumber}|{a.EmailAddress}"));
        }

        public void LoadFromFile() // Loads contacts from the  and create Contact object
        {
            if (!File.Exists(filePath)) return;
            contacts = File.ReadAllLines(filePath)
                          .Select(line => line.Split('|'))
                          .Select(parts => new Contact { FirstName = parts[0], LastName = parts[1], StreetAddress = parts[2], PostalCode = parts[3], City = parts[4], PhoneNumber = parts[5], EmailAddress = parts[6]})
                          .ToList();
        }

        internal void Delete(Contact deletableContact) //Deleates the given contact
        {
            contacts.Remove(deletableContact);
        }
    }
}