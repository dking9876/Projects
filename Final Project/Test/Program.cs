using DataLayer.DbInterfaces;
using DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
               BookTests.TestDBDeleteBookAsync().Wait();

            }
            catch(Exception ex )
            {
                Console.WriteLine(ex);
            }
        }
       
    }
}
