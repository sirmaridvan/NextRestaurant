using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NeYesekApp.Models;

namespace NeYesekApp
{
    public partial class StartThePlannig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> result = createPlan();
        }
        public List<string> createPlan()
        {
            List<Restaurant> listRestaurantDB;
            using (var ctx = new NeYesekAppContext())
            {
                listRestaurantDB = ctx.Restaurants.ToList();
            }
            //veritabanındaki restoran formatından, algoritma için istediğimiz restoran formatına geçiş
            List<Place> list = new List<Place>();

            //Bu yorum bloğu içerisindeki döngü veritabanındaki restoranlara oylama eklendiğinde aktif edilecek. Bunun yerine şimdilik el ile doldurulmuş bir liste kullanılacak.
            /*for(int i = 0; i < list.Count; i++)
            {
                list.Add(new Place(listRestaurantDB[i].Name, Convert.ToInt32(listRestaurantDB[i].Score), true));
            }*/

            //El ile doldurulmuş liste, veritabanına oylar eklendiğinde kaldırılacak.
            list.Add(new Place("a", 14, true));
            list.Add(new Place("b", 5, true));
            list.Add(new Place("c", 6, true));
            list.Add(new Place("d", 12, true));
            list.Add(new Place("e", 7, true));
            list.Add(new Place("f", 9, true));
            list.Add(new Place("g", 10, true));
            list.Add(new Place("h", 11, true));
            list.Add(new Place("j", 12, true));
            list.Add(new Place("k", 13, true));
            list.Add(new Place("l", 1, true));
            List<string> result = new List<string>();
            List<Place> availableRestaurants = new List<Place>();
            Random random = new Random();

            //100 günü temsil etmesi için 100'lük bir döngü kurdum. Normalde bu döngü olmayacak ve günlük olarak çağrı yapılacak.
            for (int i = 0; i < 100; i++)
            {
                //Seçimin yapılması istendiği gün için uygun olan restoranlar "available" listesine aktarılacak.
                //Uygun restoranların toplam dilimin yüzde kaçını kapladığı hesaplanacak."totalcapacity"
                int totalCapacity = 0;
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].isEnable())
                    {
                        availableRestaurants.Add(list[j]);
                        totalCapacity += list[j].vote;
                    }
                }
                //Eğer totalcapacity sıfır ise 100 günlük süreç tamamlanmış, gidilecek restoran kalmamış demektir.
                if (totalCapacity == 0)
                {
                    return result;
                } 
                //1 ile gidilebilecek restoranların toplan oy sayısı arasında bir değer belirlenir.
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
                for (int j = 0; j < list.Count; j++)
                {
                    if (chosen.name == list[j].name)
                    {
                        list[j].disable(availableRestaurants.Count * 7 / 25);
                        list[j].vote--;
                    }
                }
                //Ertesi gün gidilebilecek restoran değişeceği için, "availableRestaurants" listesi sıfırlanır.
                availableRestaurants.Clear();
                result.Add(chosen.name);


            }
            return result;
        }
    }
}