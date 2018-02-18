using Hello.Bob.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Bob
{
    class Program
    {
        static void Main()
        {
            DoJoo();
            Console.Read();
        }

        private static async void DoJoo()
        {
            int count = 0;
            Joo joo = new Joo();

            while (true)
            {
                count++;
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    await joo.HandleBob(count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occurred, contact someone who cares: {ex.Message}");
                }
            }
        }
    }
}
