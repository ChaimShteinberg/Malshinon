using Malshinon.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class ReportService
    {
        public static void CreateReport(Report report)
        {
            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO `report`(`reporter`, `target`, `ReportText`, `ReportingTime`) VALUES ('@Reporter','@Target','@ReportText','@ReportingTime')";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Reporter", report.Reporter);
                    command.Parameters.AddWithValue("@Target", report.Target);
                    command.Parameters.AddWithValue("@ReportText", report.ReportText);
                    command.Parameters.AddWithValue("@ReportingTime", report.ReportingTime);

                    command.ExecuteNonQuery();

                    Console.WriteLine("The report was received successfully, thank you.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
