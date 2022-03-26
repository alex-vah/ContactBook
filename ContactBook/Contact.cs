using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook
{
    public class Contact
    {
        public Contact(string name, string email, string phoneNumber, string address)
        {
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            _address = address;
        }
        public string _name { get; set; }
        public string _email { get; set; }
        public string _phoneNumber { get; set; }
        public string _address { get; set; }

        public override string ToString()
        {
            return $"Name: {_name}, Email: {_email}, Phone Number: {_phoneNumber}, Address: {_address}";
        }
    }
}
