using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class MinutelyWeather
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<MinutelyWeatherData> data { get; set; }
    }
}