using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class HourlyWeather
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<HourlyWeatherData> data { get; set; }
    }
}