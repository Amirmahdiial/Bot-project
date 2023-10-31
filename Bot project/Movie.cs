using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_project
{
    public class Movie
    {
        internal static async Task<string> PupMovie()
        {
            string str = "";

            using (HttpClient httpClient = new HttpClient())
            {
                string? stringMvi = "https://moviesapi.ir/api/v1/movies?page={page}";



                HttpResponseMessage response = await httpClient.GetAsync(stringMvi);
                if (response.IsSuccessStatusCode)
                {

                    string? movieResponse = await response.Content.ReadAsStringAsync();
                    DataRespons movieData = JsonConvert.DeserializeObject<DataRespons>(movieResponse.ToString());

                    List<ResultMovie>? result_item = movieData.Data;

                    foreach (var item in result_item)
                    {
                        str += $"\n{item.Id}:{item.Title}";

                    }
                }

                return $"please choose the id of each Movie that you want:\n{str}";
            }
            
        }
        internal static async Task<string> PupMovie(int Id)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            using (HttpClient httpClient = new HttpClient())
            {
                string? stringMvi = "https://api.wallex.ir/v1/currencies/stats";



                HttpResponseMessage response = await httpClient.GetAsync(stringMvi);
                if (response.IsSuccessStatusCode)
                {

                    string? movieResponse = await response.Content.ReadAsStringAsync();
                    DataRespons movieData = JsonConvert.DeserializeObject<DataRespons>(movieResponse.ToString());

                    List<ResultMovie>? result_item = movieData.Data;

                    if (result_item.Find(x => x.Id == Id) != null)
                    {
                        str += result_item.Find(x => x.Id == Id).Title.ToString();
                        str2 += result_item.Find(x => x.Id == Id).Poster.ToString();
                        str3 += result_item.Find(x => x.Id == Id).Year.ToString();
                        str4 += result_item.Find(x => x.Id == Id).Country.ToString();
                        str5 += result_item.Find(x => x.Id == Id).IMDB_rating.ToString();
                    }
                    else
                    {
                        str += "not found";
                        str2 += "not found";
                        str3 += "not found";
                        str4 += "not found";
                        str5 += "not found";

                    }

                }
                return $"title: {str}\nposter : {str2}\nyear : {str3}\ncountry : {str4}\nimdb_rating : {str5}";

            }
        }
                
    }
    public class ResultMovie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("poster")]
        public string? Poster { get; set; }

        [JsonProperty("year")]
        public string? Year { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("imdb_rating")]
        public string? IMDB_rating { get; set; }

        [JsonProperty("genres")]
        public List<string>? Gendres { get; set; }

        [JsonProperty("images")]
        public List<string>? Images { get; set; }
    }

    public class DataRespons
    {
        [JsonProperty("data")]
        public List<ResultMovie> Data { get; set; }
    }


}
