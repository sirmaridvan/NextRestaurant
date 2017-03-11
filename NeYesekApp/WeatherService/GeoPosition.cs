using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class GeoPosition
    {
        public int Version;
        public string Key;
        public string Type;
        public int Rank;
        public string LocalizedName;
        public string EnglishName;
        public string PrimaryPostalCodel;

        public Region Region;

        public Country Country;

        public AdminArea AdministrativeArea;

        public TimeZone TimeZone;

        public bool IsAlias;

        public List<AdminArea> SupplementalAdminAreas;

        public List<String> DataSets;

    }
}