using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.ApiWrapper;
using dashboard.Responses.Weather.WeatherResponse;
using dashboard.Responses.Weather.WeekWeatherResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dashboard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private WeatherWrapper Endpoint { get; } = new WeatherWrapper();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/weatherforecast/now/")]
        public WeatherForecast GetNow([FromQuery] string city)
        {
            Task<WeatherResponse> weather = Endpoint.FetchCurrentWeather(city);
            return new WeatherForecast
            {
                Date = DateTime.Now.DayOfWeek.ToString(),
                City = city,
                TemperatureC = (int)weather.Result.main.temp,
                MinimalC = (int)weather.Result.main.temp_min,
                MaximalC = (int)weather.Result.main.temp_max,
                Information = weather.Result.weather[0].main,
                Summary = weather.Result.weather[0].description,
                Icon = "http://openweathermap.org/img/wn/" + weather.Result.weather[0].icon + ".png"
            };
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] string city)
        {
            Task<WeekWeatherResponse> weather = Endpoint.FetchCurrentWeekWeather(city);
            return Enumerable.Range(0, 7).Select(index => new WeatherForecast
            {    
                Date = DateTime.Now.AddDays(index).DayOfWeek.ToString(),
                City = city,
                TemperatureC = (int)weather.Result.list[index].main.temp,
                MinimalC = (int)weather.Result.list[index].main.temp_min,
                MaximalC = (int)weather.Result.list[index].main.temp_max,
                Information = weather.Result.list[index].weather[0].main,
                Summary = weather.Result.list[index].weather[0].description,
                Icon = "http://openweathermap.org/img/wn/" + weather.Result.list[index].weather[0].icon + ".png"
            }).ToArray();
        }
    }
}