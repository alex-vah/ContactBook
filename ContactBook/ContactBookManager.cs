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
        private const string connectionString = "Server = localhost; Database = master; Trusted_Connection = True;"; 
        private static SqlConnection cnn = new SqlConnection(connectionString);
        private static string tableSql = "CREATE TABLE ContactBook" +
                "(Id int , Name varchar(50), Email varchar(100), PhoneNumber varchar(20), Address varchar(255))"; 
        private SqlCommand cmd = new SqlCommand(tableSql, cnn);
        private string sql;
        private SqlCommand command;
        public void Add(string name, string email, string phoneNumber, string address)
        {
            _contacts = ViewAll();
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
                List<int> ids = connection.Query<int>("SELECT Id FROM ContactBook").ToList();
                List<string> names = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                List<string> emails = connection.Query<string>("SELECT Email FROM ContactBook").ToList();
                List<string> addresses = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                List<string> phoneNumbers = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                for (int i = 0; i < names.Count(); i++)
                {
                    _contacts.Add(new Contact(names[i], emails[i], phoneNumbers[i], addresses[i]));
                }
                connection.Close();
            }
            
            Console.WriteLine($"{pos }. {_contacts[pos - 1]}");
        }

        public List<Contact> ViewAll()
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<string> names = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                List<string> emails = connection.Query<string>("SELECT Email FROM ContactBook").ToList();
                List<string> addresses = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                List<string> phoneNumbers = connection.Query<string>("SELECT Name FROM ContactBook").ToList();
                for (int i = _contacts.Count; i<names.Count(); i++)
                {
                    _contacts.Add(new Contact(names[i], emails[i], phoneNumbers[i], addresses[i]));
                }
                if(names.Count()<_contacts.Count())
                {
                    for (int i = names.Count(); i<_contacts.Count(); i++)
                    {
                        _contacts.Remove(_contacts[i]);
                    }
                }
                connection.Close();
            }
            return _contacts;
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
