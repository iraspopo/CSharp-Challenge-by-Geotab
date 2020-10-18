using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		public async Task<string []> GetRandomJokes(Tuple<string, string> names, string category, int number)
        {
            var jsonFeed = new JsonFeed("https://api.chucknorris.io");
            var results = await jsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number).ConfigureAwait(false);
            return results;
        }
        
        public async Task<string[]> GetCategories()
        {
            string [] rc = new string[50];
            if (!CachedCategories.Any())
            {
                var jsonFeed = new JsonFeed("https://api.chucknorris.io");
                rc = await jsonFeed.GetCategories().ConfigureAwait(false);
                CachedCategories.AddRange(rc);
            }
            else
            {
                rc = CachedCategories.ToArray();
            }
            return rc;
        }

        public async Task<Tuple<string, string>> GetNames()
        {
            var jsonFeed = new JsonFeed("https://www.names.privserv.com/api/");
            dynamic result = await jsonFeed.GetNames().ConfigureAwait(false);
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
