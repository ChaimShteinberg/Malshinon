using Google.Protobuf.Compiler;
using Malshinon.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class PersonService
    {
        public static Person? SearchPersonWithSecretCode(int secretCode)
        {
            Person person = new Person();
            string connectionString =
                    "server=localhost;" +
                    "user=root;" +
                    "database=MalshinonDB;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM `people` WHERE secret_code = @secretCode;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@secretCode", secretCode);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        person.Name = reader.GetString("name");
                        person.SecretCode = secretCode;
                        return person;
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static Person? SearchPersonWithName(string name)
        {
            Person person = new Person();
            string connectionString =
                    "server=localhost;" +
                    "user=root;" +
                    "database=MalshinonDB;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM `people` WHERE name = @name;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        person.Name = name;
                        person.SecretCode = reader.GetInt32("secret_Code");
                        return person;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static Person? CreatePerson(string name)
        {
            try
            {
                Person? person = new Person();

                string connectionString =
                        "server=localhost;" +
                        "user=root;" +
                        "database=MalshinonDB;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO `people`(`name`) VALUES(@name)";
                    MySqlCommand command = new MySqlCommand(@query, connection);
                    command.Parameters.AddWithValue(@"Name", name);
                    command.ExecuteNonQuery();
                }
                person = SearchPersonWithName(name);
                return person;
            }
            catch (MySqlException ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static int? SearchSecretCode(string nameOrCode)
        {
            Person? person = new Person();

            if (int.TryParse(nameOrCode, out int secretCode))
            {
                person = PersonService.SearchPersonWithSecretCode(secretCode);
                if (person == null)
                {
                    return null;
                }
            }
            else
            {
                person = PersonService.SearchPersonWithName(nameOrCode);
                if (person == null)
                {
                    Console.WriteLine("Name not pound");
                    while (true)
                    {
                        Console.Write("To create a new person?(y/n): ");
                        string answer = Console.ReadLine();

                        switch (answer)
                        {
                            case "y":
                                person = PersonService.CreatePerson(nameOrCode);
                                Console.WriteLine("A new person was created.");
                                return person.SecretCode;
                            case "n":
                                return null;
                            default:
                                Console.WriteLine("Wrong choice");
                                break;
                        }
                    }

                }
            }

            return person.SecretCode;
        }
    }
}
