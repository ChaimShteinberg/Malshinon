using Malshinon.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class ReporterService
    {
        public static void CreateReporter(int secretCode)
        {
            Reporter reporter = new Reporter();
            
            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO `reporter`(`secret_code`, `long_report_count`, `rating`) VALUES ('@secretCode','@long_report_count','@rating')";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@secretCode", secretCode);
                    command.Parameters.AddWithValue("@long_report_count", 0);
                    command.Parameters.AddWithValue("@rating", 0);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static Reporter? SearchReporter(int secretCode)
        {
            Reporter reporter = new Reporter();
            string connectionString =
                    "server=localhost;" +
                    "user=root;" +
                    "database=MalshinonDB;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM `reporter` WHERE secret_code = @secretCode;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@secretCode", secretCode);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reporter.SecretCode = secretCode;
                        reporter.LongReportCount = reader.GetInt32("long_report_count");
                        reporter.Rating = reader.GetInt32("Rating");
                        return reporter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }        
    }
}
