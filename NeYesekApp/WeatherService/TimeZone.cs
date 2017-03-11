using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp.WeatherService
{
    public class TimeZone
    {
        public string Code;
        public string Name;
        public int GmtOffset;
        public bool IsDaylightSaving;
        public DateTime NextOffsetChange;
    }
}