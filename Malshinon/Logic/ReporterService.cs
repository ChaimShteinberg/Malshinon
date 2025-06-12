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

                    string query = "INSERT INTO `reporter`(`secret_code`, `rating`) VALUES (@secret_code, @rating)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@secret_code", secretCode);
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

        public static void CalculateReporterRating(Report report)
        {
            int sum = GetSumReport(report.Reporter);
            double averageLength = GetAverageLength(report.Reporter);

            if (sum > 10)
            {
                sum = 10;
            }

            if (averageLength > 100)
            {
                averageLength = 100;
            }

            double rating = ((double) sum /4) + ((double) averageLength / 40);

            ApdateRating(report.Reporter, rating);
        }

        public static int GetSumReport(int? secretCode)
        {
            string connectiomString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB;";

            using (MySqlConnection connection = new MySqlConnection(connectiomString))
            {
                connection.Open();
                string query = "SELECT COUNT(reporter) AS sum_report_to_reporter FROM `report` WHERE reporter = @reporter;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reporter", secretCode);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32("sum_report_to_reporter");
                return count;
            }
        }

        public static double GetAverageLength(int? secretCode)
        {
            string connectiomString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB;";

            using (MySqlConnection connection = new MySqlConnection(connectiomString))
            {
                connection.Open();
                string query = "SELECT AVG(LENGTH(ReportText)) AS average_length FROM `report` WHERE reporter = @reporter;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reporter", secretCode);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    double averageLength = reader.GetInt32("average_length");
                    return averageLength;
                }
                else
                {
                    return 0;
                }
            }
        }

        static void ApdateRating(int? secretCode, double rating)
        {
            string connectiomString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB;";

            using (MySqlConnection connection = new MySqlConnection(connectiomString))
            {
                connection.Open();
                string query = "UPDATE `reporter` SET `rating`=@rating WHERE secret_code = @secretCode";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@secretCode", secretCode);
                command.Parameters.AddWithValue("@rating", rating);
                command.ExecuteNonQuery();
            }
        }
    }
}
