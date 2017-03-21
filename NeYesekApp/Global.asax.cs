using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;
using System.Web.Routing;
using NeYesekApp.WeatherService;
using Refit;
using System.Threading.Tasks;

namespace NeYesekApp
{
    public class Global : HttpApplication
    {
        //Cache consts
        public const String SEND_EMAIL = "SendMail";
        public const String CALCULATE_NEW_RESTAURANTS = "CalculateNextRestaurants";
        public const String VOTING_ENDED = "VotingEnded";
        public const String VOTING_STARTED = "VotingStarted";
        //for voting page
        public static bool IsVotingEnabled = false;

        private static DailyWeatherData todayData = null;
        private static String restaurant = "";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DoVotingStartedTask();
        }

        private static void DoVotingStartedTask()
        {
            if(HttpRuntime.Cache.Get(CALCULATE_NEW_RESTAURANTS) != null)
                HttpRuntime.Cache.Remove(CALCULATE_NEW_RESTAURANTS);


            SendEmailToAllUsers("Voting has started!", "Dear users,\nVoting has now started.\n\n\n Have fun!\n\nNeYesek!");

            Global.IsVotingEnabled = true;

            using (var ctx = new NeYesekAppContext())
            {
                ctx.UserVotes.RemoveRange(ctx.UserVotes);

                foreach (var res in ctx.Restaurants.ToList())
                {
                    res.Score = 0;
                }
                ctx.SaveChanges();
            }

            var OnCacheRemove = new CacheItemRemovedCallback(Global.CacheItemRemoved);
            HttpRuntime.Cache.Insert(Global.VOTING_ENDED, 1, null,
                    DateTime.Today.AddDays(1).AddHours(10), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        private static void DoVotingEndedTask()
        {
            IsVotingEnabled = false;

            //Burada restaurant lar eklenecek vs vs
            SendEmailToAllUsers("Voting has ended!", "Dear users,\nVoting has ended successfully. \nYou will be notified every day about your restaurants!\n\n Have fun!\n\nNeYesek!");

            using (var ctx = new NeYesekAppContext())
            {

                foreach (var res in ctx.Restaurants.Include("ScheduleInformation").ToList())

                {

                    res.ScheduleInformation.Possibility = res.Score;

                    res.ScheduleInformation.Enable = true;

                    res.ScheduleInformation.DisableDay = 0;

                }

                ctx.SaveChanges();
            }

            DoCalculateNextRestaurantsTask(10);

            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);

            HttpRuntime.Cache.Insert(VOTING_STARTED, 1, null,
                DateTime.Today.AddDays(99).AddHours(13), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        private static void DoCalculateNextRestaurantsTask(int hours)
        {

            GetWeatherInformation().Wait();
            CalculateNextRestaurants();
            Dashboard.setDailyRestaurant(restaurant);
            using(var ctx = new NeYesekAppContext())
            {
                ctx.RestaurantHistories.Add(new RestaurantHistory()
                {
                    DateAdded = DateTime.Today,
                    RestaurantName = restaurant
                });
                ctx.SaveChanges();
            }
            SendEmailToAllUsers("Today's Restaurant!", "Dear users,Today's restaurant is " + restaurant + "\n\n Have fun!\n\nNeYesek!");

            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);

            HttpRuntime.Cache.Insert(CALCULATE_NEW_RESTAURANTS, hours, null,
                DateTime.Today.AddDays(1).AddHours(hours), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        private static void CalculateNextRestaurants()
        {
            List<Restaurant> listRestaurantDB;
            using (var ctx = new NeYesekAppContext())
            {
                listRestaurantDB = ctx.Restaurants.Include("ScheduleInformation").ToList();
            }
            //veritabanındaki restoran formatından, algoritma için istediğimiz restoran formatına geçiş
            List<Place> allRestaurants = new List<Place>();

            //Bu yorum bloğu içerisindeki döngü veritabanındaki restoranlara oylama eklendiğinde aktif edilecek. Bunun yerine şimdilik el ile doldurulmuş bir liste kullanılacak.
            for (int i = 0; i < listRestaurantDB.Count; i++)
            {
                allRestaurants.Add(new Place(listRestaurantDB[i].Id, listRestaurantDB[i].Name, Convert.ToInt32(listRestaurantDB[i].ScheduleInformation.Possibility), listRestaurantDB[i].ScheduleInformation.Enable, listRestaurantDB[i].IsValidForWalking, listRestaurantDB[i].ScheduleInformation.DisableDay));
            }

            string result = "";
            List<Place> availableRestaurants = new List<Place>();
            Random random = new Random();

            //Seçimin yapılması istendiği gün için uygun olan restoranlar "available" listesine aktarılacak.
            //Uygun restoranların toplam dilimin yüzde kaçını kapladığı hesaplanacak."totalcapacity"
            int totalCapacity = 0;
            for (int j = 0; j < allRestaurants.Count; j++)
            {
                if (todayData.icon == "rain")
                {
                    if (allRestaurants[j].isEnable() && allRestaurants[j].IsValidForWalking)
                    {
                        availableRestaurants.Add(allRestaurants[j]);
                        totalCapacity += allRestaurants[j].vote;
                    }
                }
                else
                {
                    if (allRestaurants[j].isEnable())
                    {
                        availableRestaurants.Add(allRestaurants[j]);
                        totalCapacity += allRestaurants[j].vote;
                    }
                }
            }
            if (totalCapacity == 0)
            {
                //Eğer totalcapacity sıfır ise 100 günlük süreç tamamlanmış, gidilecek restoran kalmamış demektir.
                //Eğer bir ya da iki gün eksik olunursa burada kontrol edilebilir.
                return;
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

            for (int j = 0; j < allRestaurants.Count; j++)
            {
                if (chosen.name == allRestaurants[j].name)
                {
                    allRestaurants[j].disable(availableRestaurants.Count * 7 / 25);
                    allRestaurants[j].vote--;
                    if (!allRestaurants[j].IsValidForWalking)
                    {
                        for (int k = 0; k < allRestaurants.Count; k++)
                        {
                            if (!allRestaurants[k].IsValidForWalking)
                            {
                                allRestaurants[k].disable(3);
                            }
                        }
                    }
                }
            }
            using (var ctx = new NeYesekAppContext())
            {
                var db = ctx.RestaurantScheduleInfos.ToList();
                for (int k = 0; k < allRestaurants.Count; k++)
                {
                    db[k].DisableDay = allRestaurants[k].disableDay;
                    db[k].Enable = allRestaurants[k].enable;
                    db[k].Possibility = allRestaurants[k].vote;
                }
                ctx.SaveChanges();
                //allRestaurant listesi güncel restoran bilgilerine sahip. Bunu database'e yaz.
            }
            //Ertesi gün gidilebilecek restoran değişeceği için, "availableRestaurants" listesi sıfırlanır.
            availableRestaurants.Clear();
            result = chosen.name;
            restaurant = result;
        }

        public static void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            switch (k)
            {
                /*case SEND_EMAIL:
                    DoSendMailTask(Convert.ToInt32(v));
                    break;*/
                case CALCULATE_NEW_RESTAURANTS:
                    DoCalculateNextRestaurantsTask(Convert.ToInt32(v));
                    break;
                case VOTING_ENDED:
                    DoVotingEndedTask();
                    break;
                case VOTING_STARTED:
                    DoVotingStartedTask();
                    break;
            }
        }

        public static void SendEmailToAllUsers(string subject, string body)
        {
            MailMessage message = new MailMessage();
            using (var ctx = new NeYesekAppContext())
            {
                foreach (var mail in ctx.Users.Select(x => x.Email))
                {
                    if (General.IsValidEmail(mail))
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
        async static Task GetWeatherInformation()
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