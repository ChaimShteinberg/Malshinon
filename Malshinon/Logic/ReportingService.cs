using Google.Protobuf.Compiler;
using Malshinon.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Malshinon.Logic
{
    internal class ReportingService
    {
        public static void ReceiveReport()
        {
            while (true)
            {
                try
                {
                    UserReport userReport = new UserReport();
                    Console.Write("Receiving a report:\r\n" +
                        "Enter your name or secret code: ");
                    userReport.Reporter = Console.ReadLine();
                    Console.Write("Enter the target name: ");
                    userReport.Target = Console.ReadLine();
                    Console.Write("Enter the report: ");
                    userReport.Report = Console.ReadLine();
                    HandleReport(userReport);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void HandleReport(UserReport userReport)
        {
            Report report = new Report();
            Reporter? reporter = HandleReporter(userReport);
            if (reporter == null)
            {
                return;
            }
        }

        static Reporter? HandleReporter(UserReport userReport)
        {
            Reporter reporter = new Reporter();

            int? secretSecret = PersonService.SearchSecretCode(userReport.Reporter);
            if (secretSecret == null)
            {
                Console.WriteLine("Wrong choice");
                return null;
            }
            reporter.SecretCode = secretSecret;
        }

        
    }
}
