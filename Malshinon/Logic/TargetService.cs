using Malshinon.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class TargetService
    {
        public static void CreateTarget(int secretCode)
        {
            Target target = new Target();

            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO `target`(`secret_code`, `report_count`, `rating`) VALUES ('@secretCode','@report_count','@rating')";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@secretCode", secretCode);
                    command.Parameters.AddWithValue("@report_count", 0);
                    command.Parameters.AddWithValue("@rating", 0);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public static Target? SearchTarget(int secretCode)
        {
            Target target = new Target();
            string connectionString =
                    "server=localhost;" +
                    "user=root;" +
                    "database=MalshinonDB;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM `target` WHERE secret_code = @secretCode;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@secretCode", secretCode);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        target.SecretCode = secretCode;
                        target.ReportCount = reader.GetInt32("long_report_count");
                        target.Rating = reader.GetInt32("Rating");
                        return target;
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
