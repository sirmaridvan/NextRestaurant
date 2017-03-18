using NeYesekApp.Models;
using NeYesekApp.WeatherService;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private static DailyWeatherData todayData = null , tomorrowData = null , nextDayData = null;

        protected void todayButton_Click(object sender, EventArgs e)
        {
            temperature.Text = ((int)todayData.temperatureMax) + "°";
            windSpeed.Text = ((int)todayData.windSpeed) + " km/h";
        }

        protected void tomorrowButton_Click(object sender, EventArgs e)
        {
            temperature.Text = ((int)tomorrowData.temperatureMax) + "°";
            windSpeed.Text = ((int)tomorrowData.windSpeed) + " km/h";
        }

        protected void nextDayButton_Click(object sender, EventArgs e)
        {
            temperature.Text = ((int)nextDayData.temperatureMax) + "°";
            windSpeed.Text = ((int)nextDayData.windSpeed) + " km/h";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLoggedIn"] == null || Session["IsLoggedIn"] is bool == false)
            {
                Response.Redirect("/Default.aspx");
            }
            if (!this.IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(GetWeatherInformation));
            }
        }

        async Task GetWeatherInformation()
        {
            var weatherService = RestService.For<IWeatherService>(DarkSky.BaseUrl);
            var forecastResult = await weatherService.GetForecast(DarkSky.ApiKey, DarkSky.IstanbulLatitude, DarkSky.IstanbulLongtitude, DarkSky.SI_UNIT);
            if(forecastResult != null)
            {
                todayData = forecastResult.daily.data[0];
                tomorrowData = forecastResult.daily.data[1];
                nextDayData = forecastResult.daily.data[2];

                temperature.Text = ((int) todayData.temperatureMax) + "°";
                windSpeed.Text = ((int)todayData.windSpeed) + " km/h";
            }
        }
    }
}