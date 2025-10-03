using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ContactManager.Models;

namespace ContactManager.Controllers
{ 
    public class ContactController
    {
        private ContactRepository repo = new ContactRepository();

        public List<Models.Contact> GetAll() => repo.GetAll();
        public void Add(string firstName, string lastName, string streetAddress, string postalCode, string city, string emailAddress, string phoneNumber) => repo.Add(new Models.Contact { FirstName = firstName, LastName = lastName, StreetAddress = streetAddress, PostalCode = postalCode, City = city, EmailAddress = emailAddress, PhoneNumber = phoneNumber});
        public void Delete(string firstName) => repo.Delete(firstName);
        public void Delete(Contact deletableContact) => repo.Delete(deletableContact);
        public List<Models.Contact> Search(string keyword) => repo.Search(keyword);
        public void Save() => repo.SaveToFile();
        public void Load() => repo.LoadFromFile();
        public void Update(string originalName, string newFirstName, string newLastName, string newStreetAddress, string newPostalCode,string newCity, string newPhoneNumber, string newEmailAddress)
        {
            var contact = repo.GetAll().FirstOrDefault(a => a.FirstName.Equals(originalName, StringComparison.OrdinalIgnoreCase));
            if (contact != null)
            {
                contact.FirstName = newFirstName;
                contact.LastName = newLastName;
                contact.StreetAddress = newStreetAddress;
                contact.PostalCode = newPostalCode;
                contact.City = newCity;
                contact.PhoneNumber = newPhoneNumber;
                contact.EmailAddress = newEmailAddress;

            }
        }
    }
}