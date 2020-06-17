using System;

namespace dashboard
{
    public class WeatherForecast
    {
        public string Date { get; set; }
        public string City { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int MinimalC { get; set; }
        public int MaximalC { get; set; }
        public int ConvertMinimal => 32 + (int)(MinimalC / 0.5556);
        public int ConvertMaximal => 32 + (int)(MaximalC / 0.5556);
        public string Information { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
    }
}
