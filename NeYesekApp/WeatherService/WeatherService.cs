using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeYesekApp.WeatherService
{
    interface WeatherService
    {
        [Get("/locations/v1/regions")]
        Task<List<Region>> GetRegions([AliasAs("apikey")]string apiKey, [AliasAs("language")]string language);

        [Get("/locations/v1/countries/{region}")]
        Task<List<Country>> GetCountries([AliasAs("region")] string regionCode, [AliasAs("apikey")]string apiKey, [AliasAs("language")]string language);

        [Get("/locations/v1/adminareas/{country}")]
        Task<List<AdminArea>> GetAdminAreas([AliasAs("country")] string countryCode, [AliasAs("apikey")]string apiKey, [AliasAs("language")]string language, [AliasAs("offset")] int offset);

        [Get("/locations/v1/cities/geoposition/search")]
        Task<List<GeoPosition>> GetGeoPosition([AliasAs("apikey")]string apiKey, [AliasAs("language")]string language, [AliasAs("q")] string latLon, [AliasAs("details")] bool details, [AliasAs("toplevel")] bool topLevel);

        [Get("/locations/v1/cities/ipaddress")]
        Task<List<GeoPosition>> IpAddressSearch([AliasAs("apikey")]string apiKey, [AliasAs("language")]string language, [AliasAs("q")] string ipAddress, [AliasAs("details")] bool details);

        [Get("/forecasts/v1/daily/1day/{locationKey}")]
        Task<List<GeoPosition>> GetOneDayForecast([AliasAs("locationKey")]string locationKey, [AliasAs("apikey")]string apiKey, [AliasAs("language")]string language, [AliasAs("details")] bool details, [AliasAs("metric")] bool metric);


        [Get("/forecasts/v1/daily/5day/{locationKey}")]
        Task<List<GeoPosition>> GetFiveDayForecast([AliasAs("locationKey")]string locationKey, [AliasAs("apikey")]string apiKey, [AliasAs("language")]string language, [AliasAs("details")] bool details, [AliasAs("metric")] bool metric);



    }
}
