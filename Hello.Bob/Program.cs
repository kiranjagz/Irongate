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
            Task webThingy = new Task(HandleBob);
            Task fileThingy = new Task(async () => await ReadFromFile("c:\\test.txt"));
            webThingy.Start();
            fileThingy.Start();
            Console.Read();
        }

        static async void HandleBob()
        {
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

        static async Task ReadFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                //var readAllFile = await reader.ReadToEndAsync();
                //Console.WriteLine(readAllFile);

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await Task.Delay(2000);
                    Console.WriteLine(line);
                    await WriteToFile(line);
                }
            }
        }

        static async Task WriteToFile(string line)
        {
            using (StreamWriter writer = new StreamWriter("c:\\newtest.txt", true))
            {
                await writer.WriteLineAsync($"{line} new jeff");
            }
        }
    }
}
