using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        private object printValue;

        public ConsolePrinter Value(string value)
        {
            printValue = value;
            return this;
        }

        //TODO call it print and not ToString() ?

        public void Print()
        {
            Console.WriteLine(printValue);
        }
    }
}
