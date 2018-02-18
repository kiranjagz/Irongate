using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Bob.File
{
    public static class FileStuff
    {
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
