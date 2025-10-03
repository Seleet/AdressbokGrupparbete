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
    public class ContactRepository
    {
        private List<Models.Contact> contacts = new List<Models.Contact>();
        private string filePath = "Contacts.txt";

        public List<Models.Contact> GetAll() => contacts;

        public void Add(Models.Contact contact) => contacts.Add(contact);

        public void Delete(string name) =>
            contacts.RemoveAll(a => a.FirstName.Equals(name, System.StringComparison.OrdinalIgnoreCase));

        public List<Models.Contact> Search(string keyword) =>
            contacts.Where(a => a.FirstName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase) ||
                               a.LastName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)).ToList();

        public void SaveToFile()
        {
            File.WriteAllLines(filePath, contacts.Select(a => $"{a.FirstName}|{a.LastName}|{a.StreetAddress}|{a.PostalCode}|{a.City}|{a.PhoneNumber}|{a.EmailAddress}"));
        }

        public void LoadFromFile()
        {
            if (!File.Exists(filePath)) return;
            contacts = File.ReadAllLines(filePath)
                          .Select(line => line.Split('|'))
                          .Select(parts => new Contact { FirstName = parts[0], LastName = parts[1], StreetAddress = parts[2], PostalCode = parts[3], City = parts[4], PhoneNumber = parts[5], EmailAddress = parts[6]})
                          .ToList();
        }

        internal void Delete(Contact deletableContact)
        {
            contacts.Remove(deletableContact);
        }
    }
}