using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_project
{
    internal class Jokes
    {
        private static ResultJoke results;

        internal static async Task<ResultJoke> RandomJokes()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string? stringJoke = "https://api.chucknorris.io/jokes/random";

                HttpResponseMessage response = await httpClient.GetAsync(stringJoke);
                if (response.IsSuccessStatusCode)
                {

                    string? jokeResponse = await response.Content.ReadAsStringAsync();
                    
                    results =JsonConvert.DeserializeObject<ResultJoke>(jokeResponse); 
                
                }

                return results;
            }

        }
    }
    public class ResultJoke
    {
        [JsonProperty("categories")]
        public List<object> categories { get; set; }

        [JsonProperty("created_at")]
        public string? created_at { get; set; }

        [JsonProperty("icon_url")]
        public string? icon_url { get; set; }

        [JsonProperty("id")]
        public string? id { get; set; }

        [JsonProperty("updated_at")]
        public string? updated_at { get; set; }

        [JsonProperty("url")]
        public string? url { get; set; }

        [JsonProperty("value")]
        public string? value { get; set; }
    }
}
