using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class DailyWeather
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<DailyWeatherData> data { get; set; }
    }
}