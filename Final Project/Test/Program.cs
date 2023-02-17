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
                UserTests.CreateUserAsync().Wait();

            }
            catch(Exception ex )
            {
                Console.WriteLine(ex);
            }
        }
       
    }
}
