using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchDemo.Data;

namespace DataCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Demo Data in SQL Database");

            DemoDataBuilder builder = new DemoDataBuilder();

            Console.WriteLine("Starting Stock Number?");
             var startingStockString = Console.ReadLine();
             Console.WriteLine("How Many To Create?");
             var howManyString = Console.ReadLine();

             int startingStock = 0;
             int.TryParse(startingStockString, out startingStock);
             int howMany = 0;
             int.TryParse(howManyString, out howMany);

             builder.CreateRandomVehicles(howMany, startingStock);

            Console.WriteLine();
            Console.WriteLine("Complete! - Press Any Key");
            Console.ReadKey();
        }
    }
}
