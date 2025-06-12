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

                    string query = "INSERT INTO `target`(`secret_code`, `rating`) VALUES (@secretCode, @rating)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@secretCode", secretCode);
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

        public static void CalculateTargetRating(Report report)
        {
            int sum = GetSumReport(report.Target);
            bool urgent = CheckUrgency(report);

            if (sum > 20)
            {
                sum = 20;
            }
            double rating = sum / 4;

            if (urgent)
            {
                rating = 5;
            }

            ApdateRating(report.Target, rating);
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
                string query = "SELECT COUNT(target) AS sum_report_to_target FROM `report` WHERE target = @target;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@target", secretCode);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32("sum_report_to_target");
                return count;
            }
        }

        public static bool CheckUrgency(Report report)
        {
            TimeSpan timeSpan = new TimeSpan(0, 15, 0);
            DateTime dateTime2 = report.ReportingTime.Subtract(timeSpan);

            string connectiomString =
                "server=localhost;" +
                "user=root;" +
                "database=MalshinonDB;";

            using (MySqlConnection connection = new MySqlConnection(connectiomString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) AS count_DateTime FROM report AS count_DateTime WHERE ReportingTime BETWEEN @DateTime2 AND @DateTime1 AND target = @target;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@target", report.Target);
                command.Parameters.AddWithValue("@DateTime1", report.ReportingTime);
                command.Parameters.AddWithValue("@DateTime2", dateTime2);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32("count_DateTime");
                if (count >= 3)
                {
                    return true;
                }
                else
                {
                    return false;
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
                string query = "UPDATE `target` SET `rating`=@rating WHERE secret_code = @secretCode";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@secretCode", secretCode);
                command.Parameters.AddWithValue("@rating", rating);
                command.ExecuteNonQuery();
            }
        }

        public static void GetAllTarget()
        {
            List<Target> targets = new List<Target>();

            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=malshinonDB;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM target;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Target target = new Target();
                    target.SecretCode = reader.GetInt32("secret_code");
                    target.Rating = reader.GetDouble("rating");

                    targets.Add(target);
                }
                foreach (Target tar in targets)
                {
                    Console.WriteLine(tar.SecretCode);
                    Console.WriteLine(tar.Rating);
                }
            }
        }

    }
}
