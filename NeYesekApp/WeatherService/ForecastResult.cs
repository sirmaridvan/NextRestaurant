using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class ForecastResult
    {
        public float latitude { get; set;}
        public float longtitude { get; set; }
        public string timezone { get; set; }
        public int offset { get; set; }
        public CurrentWeather currently { get; set; }
        public HourlyWeather hourly { get; set; }
        public DailyWeather daily { get; set; }
        public List<Alert> alerts { get; set; }
        public Flags flags { get; set; }
    }
}