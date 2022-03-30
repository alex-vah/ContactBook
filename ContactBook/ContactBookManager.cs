using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ContactBook
{
    public class ContactBookManager
    {
        public List<Contact> _contacts = new List<Contact>();
        public static string connectionString = "Server = localhost; Database = master; Trusted_Connection = True;"; 
        private static SqlConnection cnn = new SqlConnection(connectionString);
        private string sql;
        private SqlCommand command;
        public void Add(string name, string email, string phoneNumber, string address)
        {
            var contact = new Contact(name,email,phoneNumber,address);
            _contacts.Add(contact);
            cnn.Open();
            sql = "INSERT INTO ContactBook(Id, Name, Email, PhoneNumber, Address)" +
                $"Values('{_contacts.IndexOf(contact)+1}','{name}', '{email}', '{phoneNumber}', '{address}')";
            command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
        }
        public void View(int pos)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Contact> contacts = connection.Query<Contact>("SELECT Name, Email, PhoneNumber, Address FROM ContactBook").ToList();
                for (int i = 0; i < contacts.Count(); i++)
                {
                    _contacts.Add(contacts[i]);
                }
                connection.Close();
            }
            
            Console.WriteLine($"{pos }. {_contacts[pos - 1]}");
        }

        public void ViewAll()
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Contact> contacts = connection.Query<Contact>("SELECT Name, Email, PhoneNumber, Address FROM ContactBook").ToList();
                for (int i = _contacts.Count; i<contacts.Count(); i++)
                {
                    _contacts.Add(contacts[i]);
                }
                if(contacts.Count()<_contacts.Count())
                {
                    for (int i = contacts.Count(); i<_contacts.Count(); i++)
                    {
                        _contacts.Remove(_contacts[i]);
                    }
                }
                connection.Close();
            }
            foreach (Contact contact in _contacts)
            {
                Console.WriteLine($"{_contacts.IndexOf(contact) + 1}. {contact}");
            }
        }
        public void Edit(int pos, Contact contact)
        {
            cnn.Open(); 
            sql = "UPDATE ContactBook" +
                $"SET Name = '{contact._name}', Email = '{contact._email}', PhoneNumber = '{contact._phoneNumber}', Address = '{contact._address}'" +
                $"WHERE Id = {pos}";
            command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
        }
        public void Delete(int pos)
        {
            cnn.Open();
            sql = $"DELETE ContactBook WHERE Id = '{pos}'";
            command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
