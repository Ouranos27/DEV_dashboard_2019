using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dashboard.Responses.Weather.WeatherResponse;
using dashboard.Responses.Weather.WeekWeatherResponse;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dashboard.ApiWrapper
{
    public class WeatherWrapper
    {
        public async Task<WeatherResponse> FetchCurrentWeather(string city)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client
                    .GetAsync("https://api.openweathermap.org/data/2.5/weather?q=" + city +
                              "&units=metric&appid=d190caab6bf254849d078443c74aa453").ConfigureAwait(true))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync().ConfigureAwait(true);
                    WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(result);
                    return weather;
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }

            return null;
        }

        public async Task<WeekWeatherResponse> FetchCurrentWeekWeather(string city)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client
                    .GetAsync("https://api.openweathermap.org/data/2.5/forecast?q=" + city +
                              "&units=metric&appid=d190caab6bf254849d078443c74aa453").ConfigureAwait(true))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync().ConfigureAwait(true);
                    Console.WriteLine(result);
                    WeekWeatherResponse listWeather = JsonConvert.DeserializeObject<WeekWeatherResponse>(result);
                    return listWeather;
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