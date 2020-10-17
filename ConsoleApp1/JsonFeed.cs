using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";

        public JsonFeed() { }
        //TODO - igor-result is used for what? - remove?
		public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }


		public static string[] GetRandomJokes(
            string firstName,
            string lastName,
            string category,
            int numOfJokes)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            string url = "jokes/random";
            if (category != null)
            {
                if (url.Contains('?'))
                    url += "&";
                else url += "?";
                url += "category=";
                url += category;
            }
			string[] rc = new string[numOfJokes];

            for (int i = 0 ; i< numOfJokes; i++)
			{
				//TODO - make it more efficient async. C#8 has async streams
				string jokeAsJsonStr = Task.FromResult(client.GetStringAsync(url).Result).Result;
				string joke = JsonConvert.DeserializeObject<dynamic>(jokeAsJsonStr).value;
				//it is better to replace string after we extract value
				//TODO - check if one of them can be null but not the other?
				if (firstName != null && lastName != null)
            	{
                	//TODO 1 - make this case insensitive comparison. Some jokes have upper case Chuck Norris
					//TODO 2 - replace all occurrences of Chuck and Norris anywhere in the joke. Use replace?
					var regex = new Regex("Chuck", RegexOptions.IgnoreCase); 
					joke = regex.Replace(joke, firstName);
					regex = new Regex("Norris", RegexOptions.IgnoreCase); 
					joke = regex.Replace(joke, lastName);	

					// int index = joke.IndexOf("Chuck Norris");
                	// string firstPart = joke.Substring(0, index);
                	// string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                	// joke = firstPart + " " + firstname + " " + lastname + secondPart;
            	}
				rc[i] = joke;
			}
			return rc;
        }
        //igor - fixed : CamelCase function name, comment was wrong
        //TODO- fix comment
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static dynamic GetNames()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var result = client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        public static string[] GetCategories()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            //igor - end point to categories - was missing
            string url = "jokes/categories";
            var result = client.GetStringAsync(url).Result;
            //igor - deserialize to proper type ->string[]
            return JsonConvert.DeserializeObject<string[]>(result);
        }
    }
}
