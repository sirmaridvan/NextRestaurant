using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp
{
    public class Place
    {
        public string name;
        public int vote;
        public bool enable;
        public int disableDay;
        public Place(string name, int vote, bool enable)
        {
            this.name = name;
            this.vote = vote;
            this.enable = enable;
        }
        public void disable(int disableDay)
        {
            this.enable = false;
            this.disableDay = disableDay;
        }
        public bool isEnable()
        {
            if (enable)
            {
                return true;
            }
            else
            {
                disableDay--;
                if (disableDay == 0)
                {
                    enable = true;
                }
                return false;
            }
        }
    }
}
