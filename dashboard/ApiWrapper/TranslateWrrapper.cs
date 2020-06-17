 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Net.Http;
 using System.Threading.Tasks;
 using dashboard.Models;
 using dashboard.Responses.Weather.WeatherResponse;
 using Google.Cloud.Translation.V2;
 using Newtonsoft.Json;

 namespace dashboard.ApiWrapper
{
    public class TranslateWrrapper
    {
        public async Task<TranslateModel> FetchTranslate(string from, string to, string text)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(
                    "https://translation.googleapis.com/language/translate/v2?key=AIzaSyDnnlklIb2lddAcerawuzZJEz7BIovYOM4&q=" +
                    text + "&source=" + from + "&target=" + to).ConfigureAwait(true))
                    
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync().ConfigureAwait(true);
                    TranslateModel translation = JsonConvert.DeserializeObject<TranslateModel>(result);
                    return translation;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
