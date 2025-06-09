using Malshinon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Logic
{
    internal class ReportingService
    {
        static void ReceiveReport()
        {
            UserReport userReport = new UserReport();
            Console.Write("Receiving a report:\r\n" +
                "Enter your name or secret code: ");
            userReport.Reporter = Console.ReadLine();
            Console.Write("Enter the target name: ");
            userReport.Target = Console.ReadLine();
            Console.Write("Enter the report: ");
            userReport.Report = Console.ReadLine();
        }
    }
}
