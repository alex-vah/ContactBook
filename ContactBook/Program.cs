using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace ContactBook
{
    public class ConnectionString
    {
        public string connectionString { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionstring = new ConnectionString();
            string jsonString = JsonSerializer.Serialize(connectionstring);
            ContactBookManager manager = new ContactBookManager();
            while (true)
            {
                Console.WriteLine(@"Enter the operation you want to do: Create new contact(Create), 
                                    View a single contact(View), 
                                    View all contacts(View all), 
                                    Edit a contact(Edit),
                                    Delete(Delete)");
                string op = Console.ReadLine();
                if (op.ToLower() == "create")
                {
                    Console.Write("Enter the name of your contact: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter the email of your contact: ");
                    string email = Console.ReadLine();
                    while(IsValidEmail(email)==false)
                    {
                        Console.WriteLine("Invalid email. Try Again");
                        email = Console.ReadLine();
                    }
                    Console.Write("Enter the phone number of your contact: ");
                    string phoneNumber = Console.ReadLine();
                    Console.Write("Enter the address of your contact: ");
                    string address = Console.ReadLine();
                    manager.Add(name, email, phoneNumber, address);
                }
                else if (op.ToLower() == "view")
                {
                    Console.Write("Enter the position of the contact you want to view: ");
                    int pos = int.Parse(Console.ReadLine());
                    manager.View(pos);
                }
                else if (op.ToLower().Trim() == "view all")
                {
                    manager.ViewAll();
                }
                else if (op.ToLower() == "edit")
                {
                    Console.Write("Enter the position of the contact you want to edit: ");
                    int pos = int.Parse(Console.ReadLine());
                    Console.Write("Enter the new name of your contact: ");
                    string name2 = Console.ReadLine();
                    Console.Write("Enter the new email of your contact: ");
                    string email2 = Console.ReadLine();
                    Console.Write("Enter the new phone number of your contact: ");
                    string phoneNumber2 = Console.ReadLine();
                    Console.Write("Enter the new address of your contact: ");
                    string address2 = Console.ReadLine();
                    Contact contact = new Contact(name2, email2, phoneNumber2, address2);
                    manager.Edit(pos, contact);
                }
                else if(op.ToLower()=="delete")
                {
                    Console.WriteLine("Enter the position of contact you want to delete");
                    int pos = int.Parse(Console.ReadLine());
                    manager.Delete(pos);
                }
                else
                {
                    Console.WriteLine("Invalid operation. Try again");
                }
            }
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
