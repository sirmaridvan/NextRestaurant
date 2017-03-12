using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class DailyWeatherData
    {
        public long time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public long sunriseTime { get; set; }
        public long sunsetTime { get; set; }
        public float moonPhase { get; set; }
        public float precipIntensity { get; set; }
        public float precipIntensityMax { get; set; }
        public long precipIntensityMaxTime { get; set; }
        public float precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperatureMin { get; set; }
        public double temperatureMax { get; set; }
        public long temperatureMinTime { get; set; }
        public long temperatureMaxTime { get; set; }
        public double apparentTemperatureMin { get; set; }
        public double apparentTemperatureMax { get; set; }
        public long apparentTemperatureMinTime { get; set; }
        public long apparentTemperatureMaxTime { get; set; }
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