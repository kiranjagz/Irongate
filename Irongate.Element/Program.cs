using Autofac;
using Irongate.Element.Root;
using Irongate.Element.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element
{
    class Program
    {
        static void Main(string[] args)
        {
            IElementRoot root = new ElementRoot();
            root.Start();

            Console.WriteLine("Some rabbit processing is happening...I think..:|");
            Console.Read();
        }
    }
}
