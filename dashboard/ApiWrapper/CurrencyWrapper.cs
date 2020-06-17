using System;
using System.Net.Http;
using System.Threading.Tasks;
using dashboard.Responses.CurrencyModel;
using Dashboard.Responses.WeatherModel.WeatherResponse;
using Newtonsoft.Json;

namespace dashboard.ApiWrapper
{
    public class CurrencyWrapper
    {
        public CurrencyResponse    rates { get; set; }
        public async Task<CurrencyResponse> FetchCurrentWeather(string first, string second)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response =
                    await client.GetAsync(
                        "http://www.apilayer.net/api/live?access_key=cf06b5b32fa1da69f8f3ca37276a4899"))
                using (HttpContent content = response.Content)
                {    
                    string result = await content.ReadAsStringAsync();
                    rates = JsonConvert.DeserializeObject<CurrencyResponse>(result);
                    return rates;
                }
            }
               catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            return null;
        }
    }
}