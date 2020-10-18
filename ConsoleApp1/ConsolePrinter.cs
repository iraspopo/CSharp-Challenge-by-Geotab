using System;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        private object printValue;

        public ConsolePrinter()
        {
        }

        public ConsolePrinter Value(string value)
        {
            this.printValue = value;
            return this;
        }

        //igor - call it Print and not ToString() - right?

        public void Print()
        {
            Console.WriteLine(printValue);
        }

        public void PrintResults(string [] results)
        {
            Value("[" + string.Join(",", results) + "]").Print();
        }
        public void PrintResultsPerLine(string [] results)
        {
            int count = 1;
            foreach (string r in results)
            {
                Value(count.ToString() + ". " + r).Print();
                count++;
            }
        } 
    }
}
