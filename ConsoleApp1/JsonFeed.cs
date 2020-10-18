using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class JsonFeed
    {
        private string _url = "";

        public JsonFeed() { }

		public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }


		public string[] GetRandomJokes(
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
				string jokeAsJsonStr = Task.FromResult(client.GetStringAsync(url).Result).Result;
				string joke = JsonConvert.DeserializeObject<dynamic>(jokeAsJsonStr).value;

				if (firstName != null && lastName != null)
            	{
					var regex = new Regex("Chuck", RegexOptions.IgnoreCase); 
					joke = regex.Replace(joke, firstName);
					regex = new Regex("Norris", RegexOptions.IgnoreCase); 
					joke = regex.Replace(joke, lastName);	
            	}
				rc[i] = joke;
			}
			return rc;
        }

        public dynamic GetNames()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var result = client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        public string[] GetCategories()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            string url = "jokes/categories";
            var result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<string[]>(result);
        }
    }
}
