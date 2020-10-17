using System.Linq.Expressions;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static char key;
        //igor - remove this. What is the point of this when mentod can return names
        static Tuple<string, string> names;

        static List<string> categories = new List<string>();
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            //what is the purpose of this prompt - TODO -igor -remove and add option below to exit the game with x
            printer.Value("Welcome to Chuck Norris jokes game!").ToString();
            //printer.Value("Press ? to get instructions.").ToString();
            //if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.Value("Press c to get categories").ToString();
                    printer.Value("Press r to get random jokes").ToString();
                    printer.Value("Press x to exit").ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        //TODO - igor make this one function
                        getCategories();
                        PrintResults();
                    }
                    if (key == 'r')
                    {
                        //igor - optimize the code
                        string category = null;
                        int numOfJokes = 1;

                        printer.Value("Want to use a random name? y/n").ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            GetNames();

                        printer.Value("Want to specify a category? y/n").ToString();
                        //Igor - missed to get the category
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            //IGOR - USER EXPERIENCE - THEY WILL NOT NOW CATEGORIES
                            getCategories();
                            PrintResults();
                            //TODO - IGOR - validate correct input and keep prompting until valid
                            printer.Value("Enter a category:").ToString();                          
                            category = Console.ReadLine();
                        }
                        //TODO - validate if user input is correct (number and in valid range) 
                        printer.Value("How many jokes do you want? (1-9)").ToString();
                        numOfJokes = Int32.Parse(Console.ReadLine());

                        GetRandomJokes(category, numOfJokes);
                        PrintResultsPerLine();
                    }
                    if (key == 'x')
                    {
                        printer.Value("Thanks for playing").ToString();
                        Environment.Exit(0);
                    }
                    names = null;
                }
            }

        }

        private static void PrintResults()
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
        }
        private static void PrintResultsPerLine()
        {
            int count = 1;
            foreach (string r in results)
            {
                printer.Value(count.ToString() + ". " + r).ToString();
                count++;
            }
        }        

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            //reset key
            key = ' ';
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
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
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.X:
                    key = 'x';
                    break;    
            }
            //Igor - easier to read
            Console.WriteLine();
        }

        private static void GetRandomJokes(string category, int number)
        {
            new JsonFeed("https://api.chucknorris.io");
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number);
        }

        private static void getCategories()
        {
            if (!categories.Any())
            {
                new JsonFeed("https://api.chucknorris.io");
                results = JsonFeed.GetCategories();
                categories.AddRange(results);
            }
            else
            {
                results = categories.ToArray();
            }

        }

        //igor - make it with param that indicates random vs input names
        private static void GetNames()
        {
            new JsonFeed("https://www.names.privserv.com/api/");
            dynamic result = JsonFeed.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
