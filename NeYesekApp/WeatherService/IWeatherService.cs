using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeYesekApp.WeatherService
{
    interface IWeatherService
    {
        [Get("/forecast/{apikey}/{latitude},{longtitude}")]
        Task<ForecastResult> GetForecast([AliasAs("apikey")]string apiKey, [AliasAs("latitude")]double latitude, [AliasAs("longtitude")]double longtitude, [AliasAs("units")]string unit);
    }
}
