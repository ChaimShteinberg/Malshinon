using Malshinon.Logic;
using Malshinon.Models;

namespace Malshinon
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportingService.ReceiveReport();
            
            //Person a = SearchPerson.SearchPersonWithSecretCode(2);
            //Console.WriteLine(a.Name);
            //Console.WriteLine(a.SecretCode);

            //Person b = SearchPerson.SearchPersonWithName("chaim");
            //Console.WriteLine(b.Name);
            //Console.WriteLine(b.SecretCode);
        }
    }
}