using Malshinon.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class AlertService
    {
        public static void TurnAlert(int? target, string message)
        {
            Alert alert = new Alert(target, message);

            string connectionString =
                "server=localhost;" +
                "user=root;" +
                "database=malshinonDB;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO `Alert`(`target`, `message`) VALUES (@target, @message)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@target", target);
                command.Parameters.AddWithValue("@message", message);
                command.ExecuteNonQuery();
            }
        }
    }
}
