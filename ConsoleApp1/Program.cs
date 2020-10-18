using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static List<int> validNumberOfJokes = new List<int>{1,2,3,4,5,6,7,8,9};
        static void Main(string[] args)
        {
            ConsolePrinter printer = new ConsolePrinter();
            ChuckNorrisController controller = new ChuckNorrisController();

            var donePlaying = false;
            printer.Value("").Print();//new line
            printer.Value("Lets get some Chuck Norris jokes!").Print();
            while (!donePlaying)
            {
                //igor - get the joke (no questions asked) or customize your jokes
                printer.Value("Press 1 - Get one Chuck Norris joke").Print();
                printer.Value("Press 2 - Make customized Chuck Norris joke/s").Print();
                printer.Value("Press x - Exit").Print();
                
                char mainSelectionKey = GetEnteredKey(Console.ReadKey());

                string categorySelected = null;
                int numOfJokes = 0;
                Tuple<string, string> names = null;

                //get just one joke
                if (mainSelectionKey == '1')
                {
                    numOfJokes = 1;
                }
                //customized jokes
                if (mainSelectionKey == '2')
                {
                    printer.Value("Want to use a random name? y/n").Print();
                    char nameSelectionKey = GetEnteredKey(Console.ReadKey());
                    if (nameSelectionKey == 'y')
                        names = controller.GetNames();

                    printer.Value("Want to specify a category? y/n").Print();
                    char categorySelectionKey = GetEnteredKey(Console.ReadKey());
                    if (categorySelectionKey == 'y')
                    {
                        //present categories and check input
                        categorySelected = GetCategorySelection(controller, printer);
                    }

                    numOfJokes = GetNumberOfJokes(printer);

                }
                if (mainSelectionKey == '1' || mainSelectionKey == '2')
                {
                    printer.Value("Getting joke/s...").Print();
                    var jokes = controller.GetRandomJokes(names, categorySelected, numOfJokes);
                    printer.PrintResultsPerLine(jokes);
                }
                if (mainSelectionKey == 'x')
                {
                    printer.Value("Thanks for playing").Print();
                    donePlaying = true;
                }
            }
        }

        private static string GetCategorySelection(ChuckNorrisController controller, ConsolePrinter printer)
        {
            bool validCategory = false;
            string rc = "";
            while (!validCategory)
            {
                printer.PrintResults(controller.GetCategories());
                printer.Value("Enter a category:").Print();
                string categoryInput = Console.ReadLine();
                if (controller.CachedCategories.Contains(categoryInput))
                {
                    validCategory = true;
                    rc = categoryInput;
                }
                else
                {
                    printer.Value("Invalid catergory entered. Please enter a category from this list below:").Print();
                }
            }
            return rc;

        }     

        private static int GetNumberOfJokes(ConsolePrinter printer)
        {
            bool validSelection = false;
            int num = 1;
            while (!validSelection)
            {
                printer.Value("How many jokes do you want? (1-9)").Print();
                char input = GetEnteredKey(Console.ReadKey());
                int numberOfJokes = Convert.ToInt32(Char.GetNumericValue(input));
                if (validNumberOfJokes.Contains(numberOfJokes))
                {
                    validSelection = true;
                    num = numberOfJokes;
                }
            }
            return num;

        }
        private static char GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            //reset key
            char key = ' ';
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;  
                case ConsoleKey.D2:
                    key = '2';
                    break;    
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;                    
                case ConsoleKey.X:
                    key = 'x';
                    break;    
            }
            //Igor - easier to read with new line after this
            Console.WriteLine();
            return key;
        }
    }
}
