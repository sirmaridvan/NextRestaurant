using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using NeYesekApp.WeatherService;
using Refit;
using System.Threading.Tasks;

namespace NeYesekApp
{
    public class Global : HttpApplication
    {
        public const String SEND_EMAIL = "SendMail";
        public const String CALCULATE_NEW_RESTAURANTS = "CalculateNextRestaurants";
        private static DailyWeatherData todayData = null;
        private String restaurant="";

        async void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DoSendMailTask(10);
            DoCalculateNextRestaurantsTask(10);
            await GetWeatherInformation();
            CalculateNextRestaurants();
            Dashboard.setDailyRestaurant(restaurant);
        }

        private void DoSendMailTask(int hours)
        {
            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);

            if (DateTime.Now.Hour < hours)
            {
                HttpRuntime.Cache.Insert(SEND_EMAIL, hours, null,
                    DateTime.Today.AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
            else
            {
                HttpRuntime.Cache.Insert(SEND_EMAIL, hours, null,
                    DateTime.Today.AddDays(1).AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
        }
        private void DoCalculateNextRestaurantsTask(int hours)
        {
            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);

            if (DateTime.Now.Hour < hours)
            {
                HttpRuntime.Cache.Insert(CALCULATE_NEW_RESTAURANTS, hours, null,
                    DateTime.Today.AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
            else
            {
                HttpRuntime.Cache.Insert(CALCULATE_NEW_RESTAURANTS, hours, null,
                    DateTime.Today.AddDays(1).AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
        }

        private void CalculateNextRestaurants()
        {
            List<Restaurant> listRestaurantDB;
            List<RestaurantScheduleInfo> listRestaurantSchedule;
            using (var ctx = new NeYesekAppContext())
            {
                listRestaurantDB = ctx.Restaurants.ToList();
                listRestaurantSchedule = ctx.RestaurantScheduleInfos.ToList();
                ctx.SaveChanges();
            }
            //veritabanındaki restoran formatından, algoritma için istediğimiz restoran formatına geçiş
            List<Place> allRestaurant = new List<Place>();

            //Bu yorum bloğu içerisindeki döngü veritabanındaki restoranlara oylama eklendiğinde aktif edilecek. Bunun yerine şimdilik el ile doldurulmuş bir liste kullanılacak.
            for (int i = 0; i < listRestaurantDB.Count; i++)
            {
                allRestaurant.Add(new Place(listRestaurantDB[i].Id,listRestaurantDB[i].Name, Convert.ToInt32(listRestaurantSchedule[i].Possibility), listRestaurantSchedule[i].Enable, listRestaurantDB[i].IsValidForWalking,listRestaurantSchedule[i].DisableDay));
            }

            string result = "";
            List<Place> availableRestaurants = new List<Place>();
            Random random = new Random();

            //Seçimin yapılması istendiği gün için uygun olan restoranlar "available" listesine aktarılacak.
            //Uygun restoranların toplam dilimin yüzde kaçını kapladığı hesaplanacak."totalcapacity"
            int totalCapacity = 0;
            for (int j = 0; j < allRestaurant.Count; j++)
            {
                if (todayData.icon == "rain")
                {
                    if (allRestaurant[j].isEnable() && allRestaurant[j].IsValidForWalking)
                    {
                        availableRestaurants.Add(allRestaurant[j]);
                        totalCapacity += allRestaurant[j].vote;
                    }
                }
                else
                {
                    if (allRestaurant[j].isEnable())
                    {
                        availableRestaurants.Add(allRestaurant[j]);
                        totalCapacity += allRestaurant[j].vote;
                    }
                }
            }
            if (totalCapacity == 0)
            {
                //Eğer totalcapacity sıfır ise 100 günlük süreç tamamlanmış, gidilecek restoran kalmamış demektir.
                //Eğer bir ya da iki gün eksik olunursa burada kontrol edilebilir.
            }
            //1 ile gidilebilecek restoranların toplam oy sayısı arasında bir değer belirlenir.
            int rand = random.Next(1, totalCapacity + 1);
            int counter = 0;
            int interval = 0;
            //Bu değere bağlı kalınarak gün içinde gidilecek restoran seçilir.
            while (interval < rand)
            {
                interval += availableRestaurants[counter].vote;
                counter++;
            }
            //Gidilecek restorana belirli bir süre gidilememesi için restoranın durumu olumsuz yapılır.
            //Toplam gidileceği gün sayısı bir azaltılır.
            Place chosen = availableRestaurants[counter - 1];

            for (int j = 0; j < allRestaurant.Count; j++)
            {
                if (chosen.name == allRestaurant[j].name)
                {
                    allRestaurant[j].disable(availableRestaurants.Count * 7 / 25);
                    allRestaurant[j].vote--;
                    if (!allRestaurant[j].IsValidForWalking)
                    {
                        for (int k = 0; k < allRestaurant.Count; k++)
                        {
                            if (!allRestaurant[k].IsValidForWalking)
                            {
                                allRestaurant[k].disable(3);
                            }
                        }
                    }
                }
            }
            using (var ctx = new NeYesekAppContext())
            {
                var db = ctx.RestaurantScheduleInfos.ToList();
                for (int k = 0; k < allRestaurant.Count; k++)
                {
                    db[k].DisableDay = allRestaurant[k].disableDay;
                    db[k].Enable = allRestaurant[k].enable;
                    db[k].Possibility = allRestaurant[k].vote;
                }
                ctx.SaveChanges();
                //allRestaurant listesi güncel restoran bilgilerine sahip. Bunu database'e yaz.
            }
            //Ertesi gün gidilebilecek restoran değişeceği için, "availableRestaurants" listesi sıfırlanır.
            availableRestaurants.Clear();
            result = chosen.name;
            restaurant=result;
        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            switch (k)
            {
                case SEND_EMAIL:
                    DoSendMailTask(Convert.ToInt32(v));
                    break;
                case CALCULATE_NEW_RESTAURANTS:
                    DoCalculateNextRestaurantsTask(Convert.ToInt32(v));
                    break;
            }
        }

        public void SendEmail(string subject, string body)
        {
            MailMessage message = new MailMessage();
            using (var ctx = new NeYesekAppContext())
            {
                foreach (var mail in ctx.Users.Select(x => x.Email))
                {
                    message.To.Add(new MailAddress(mail));
                }
            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.Body += Environment.NewLine;
            message.Body += Environment.NewLine;

            var client = new SmtpClient();
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {

            }
        }
        async Task GetWeatherInformation()
        {
            var weatherService = RestService.For<IWeatherService>(DarkSky.BaseUrl);
            var forecastResult = await weatherService.GetForecast(DarkSky.ApiKey, DarkSky.IstanbulLatitude, DarkSky.IstanbulLongtitude, DarkSky.SI_UNIT);
            if (forecastResult != null)
            {
                todayData = forecastResult.daily.data[0];
            }
        }
    }
}