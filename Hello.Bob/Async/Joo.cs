using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Bob.Async
{
    public class Joo
    {

        public async Task HandleBob(int number)
        {
            if (number % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception($"Number has a reminder of zero, boom ERROR SIR!!!, {number}");
            }

            Console.WriteLine($"Waiting 2 seconds for a miracle, {number}");
            await Task.Delay(TimeSpan.FromSeconds(2));

            string url = "http://en.wikipedia.org/";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(content))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }
    }
}
