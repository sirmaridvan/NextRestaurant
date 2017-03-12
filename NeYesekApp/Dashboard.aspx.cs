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
            var forecastResult = await weatherService.GetForecast(DarkSky.ApiKey, DarkSky.IstanbulLatitude, DarkSky.IstanbulLongtitude);
            if(forecastResult != null)
            {

            }
        }
    }
}