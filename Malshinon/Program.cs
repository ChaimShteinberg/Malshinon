using Malshinon.Logic;
using Malshinon.Models;
using MySql.Data.MySqlClient;

namespace Malshinon
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportingService.ReceiveReport();

            //TargetService.GetAllTarget();

        }
    }
}