using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class MinutelyWeatherData
    {
        public DateTime time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public float precipIntensity { get; set; }
        public float precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPoint { get; set; }
        public float humidity { get; set; }
        public double windSpeed { get; set; }
        public int windBearing { get; set; }
        public float visibility { get; set; }
        public float cloudCover { get; set; }
        public double pressure { get; set; }
        public double ozone { get; set; }
    }
}