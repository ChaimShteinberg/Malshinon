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
                    userReport.ReportText = Console.ReadLine();
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
            report.Reporter = reporter.SecretCode;

            Target target = HandleTarget(userReport);
            if (target == null)
            {
                return;
            }
            report.Target = target.SecretCode;

            report.ReportText = userReport.ReportText;
            report.ReportingTime = userReport.ReportingTime;

            ReportService.CreateReport(report);
        }

        static Reporter? HandleReporter(UserReport userReport)
        {
            

            Person person = PersonService.SearchPerson(userReport.Reporter);
            if (person == null)
            {
                return null;
            }
            int secretCode = person.SecretCode;

            Reporter? reporter = ReporterService.SearchReporter(secretCode);
            if (reporter == null)
            {
                ReporterService.CreateReporter(secretCode);
                reporter = ReporterService.SearchReporter(secretCode);
            }
            //עדיין לא טופל בדירוג

            return reporter;
        }

        static Target? HandleTarget(UserReport userReport)
        {


            Person person = PersonService.SearchPerson(userReport.Reporter);
            if (person == null)
            {
                return null;
            }
            int secretCode = person.SecretCode;

            Target? target = TargetService.SearchTarget(secretCode);
            if (target == null)
            {
                TargetService.CreateTarget(secretCode);
                target = TargetService.SearchTarget(secretCode);
            }

            //עדיין לא טופל בדירוג

            return target;
        }

    }
}
