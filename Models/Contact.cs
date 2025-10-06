using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models 
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
       

        public override string ToString() => $"{FirstName} {LastName}, lives at {StreetAddress} {PostalCode} {City}\nPhone Number:{PhoneNumber}\nEmail Address:{EmailAddress}"; //Override replaces the base class method
    }
}