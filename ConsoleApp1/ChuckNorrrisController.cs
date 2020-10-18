using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class ChuckNorrisController
    {
        private List<string> cachedCategories = new List<string>();
        public ChuckNorrisController() { }

        public List<string> CachedCategories 
        {
            get{return cachedCategories;}
            set{cachedCategories = value;}
        }

		public string [] GetRandomJokes(Tuple<string, string> names, string category, int number)
        {
            var jsonFeed = new JsonFeed("https://api.chucknorris.io");
            var results = jsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number);
            return results;
        }
        
        public string[] GetCategories()
        {
            string [] rc = new string[50];
            if (!CachedCategories.Any())
            {
                var jsonFeed = new JsonFeed("https://api.chucknorris.io");
                rc = jsonFeed.GetCategories();
                CachedCategories.AddRange(rc);
            }
            else
            {
                rc = CachedCategories.ToArray();
            }
            return rc;
        }

        //igor - make it with param that indicates random vs input names
        public  Tuple<string, string> GetNames()
        {
            var jsonFeed = new JsonFeed("https://www.names.privserv.com/api/");
            dynamic result = jsonFeed.GetNames();
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
