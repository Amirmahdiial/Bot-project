using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot_project
{
    internal class Cryptocurrency
    {
        internal static async Task<string> CryptoAsync()
        {
            string str = "";

            using (HttpClient httpClient = new HttpClient())
            {
                string? stringApi = "https://api.wallex.ir/v1/currencies/stats";



                HttpResponseMessage response = await httpClient.GetAsync(stringApi);
                if (response.IsSuccessStatusCode)
                {

                    string? apiresponse = await response.Content.ReadAsStringAsync();
                    Apirespons apidata = JsonConvert.DeserializeObject<Apirespons>(apiresponse.ToString());

                    List<Result_Items>? result_item = apidata.result;

                    foreach (var item in result_item)
                    {
                        str += $"\n{item.rank}:{item.name_en}";

                    }
                }

                return $"please choose the rank of each coin that you want:\n{str}";
            }
        }
               

        internal static async Task<string> CryptoAsync(int rank)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            using (HttpClient httpClient = new HttpClient())
            {
                string? stringApi = "https://api.wallex.ir/v1/currencies/stats";



                HttpResponseMessage response = await httpClient.GetAsync(stringApi);
                if (response.IsSuccessStatusCode)
                {

                    string? apiresponse = await response.Content.ReadAsStringAsync();
                    Apirespons apidata = JsonConvert.DeserializeObject<Apirespons>(apiresponse.ToString());

                    List<Result_Items>? result_item = apidata.result;

                    if (result_item.Find(x => x.rank == rank) != null)
                    {
                        str += result_item.Find(x => x.rank == rank).price.ToString();
                        str2 += result_item.Find(x => x.rank == rank).type.ToString();
                        str3 += result_item.Find(x => x.rank == rank).created_at.ToString();
                        str4 += result_item.Find(x => x.rank == rank).name_en.ToString();
                    }
                    else
                    {
                        str += "not found";
                        str2 += "not found";
                        str3 += "not found";
                        str4 += "not found";

                    }

                }
                return $"name: {str4}\nprice : {str}\ntype : {str2}\ncreated_at : {str3}";

            }
        }
                
    }
    public class Apirespons
    {

        public List<Result_Items> result { get; set; }


    }
    public class Result_Items
    {
        public string? key { get; set; }
        public string? name { get; set; }
        public string? name_en { get; set; }
        public int? rank { get; set; }
        public double? dominance { get; set; }
        public double? volume_24h { get; set; }
        public double? market_cap { get; set; }
        public double? ath { get; set; }
        public double? atl { get; set; }
        public double? ath_change_percentage { get; set; }
        public DateTime? ath_date { get; set; }
        public double? price { get; set; }
        public double? daily_high_price { get; set; }
        public double? daily_low_price { get; set; }
        public double? weekly_high_price { get; set; }
        public double? monthly_high_price { get; set; }
        public double? yearly_high_price { get; set; }
        public double? weekly_low_price { get; set; }
        public double? monthly_low_price { get; set; }
        public double? yearly_low_price { get; set; }
        public double? percent_change_1h { get; set; }
        public double? percent_change_24h { get; set; }
        public double? percent_change_7d { get; set; }
        public double? percent_change_14d { get; set; }
        public double? percent_change_30d { get; set; }
        public double? percent_change_60d { get; set; }
        public double? percent_change_200d { get; set; }
        public double? percent_change_1y { get; set; }
        public double? price_change_24h { get; set; }
        public double? price_change_7d { get; set; }
        public double? price_change_14d { get; set; }
        public double? price_change_30d { get; set; }
        public double? price_change_60d { get; set; }
        public double? price_change_200d { get; set; }
        public double? price_change_1y { get; set; }
        public double? max_supply { get; set; }
        public double? total_supply { get; set; }
        public double? circulating_supply { get; set; }
        public string? type { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
