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


        public async Task<string[]> GetRandomJokes(
            string firstName,
            string lastName,
            string category,
            int numOfJokes)
        {
            try
            {
                using (var client = new HttpClient())
                {
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
                    ConsolePrinter printer = new ConsolePrinter();
                    for (int i = 0; i < numOfJokes; i++)
                    {
                        var jokeAsJsonStr = await client.GetStringAsync(url).ConfigureAwait(false);
                        string joke = JsonConvert.DeserializeObject<dynamic>(jokeAsJsonStr).value;
                        
                        printer.Value("Getting jokes " + new String('.',i+1)).Print();

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
            }
            catch (Exception ex)
            { 
                return new string[2]{ "Sorry, no Chuck jokes for you at this time.", ex.Message};
            }

        }

        public async Task<dynamic> GetNames()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    var result = await client.GetStringAsync("").ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<dynamic>(result);
                }
            }
            catch (Exception ex)
            {
                return new string[2]{ "Sorry, can't get random names for you. Must be Chuck's doing :).", ex.Message};
            }
        }

        public async Task<string[]> GetCategories()
        {
            try 
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string url = "jokes/categories";
                    var result = await client.GetStringAsync(url).ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<string[]>(result);
                }
            }
            catch(Exception ex)
            {
                return new string[2]{"Sorry, can't get Chuck joke categories. Don't tell Chuck.", ex.Message};
            }
        }
    }
}
