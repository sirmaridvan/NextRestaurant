using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeYesekApp
{
    public class Place
    {
        public int id;
        public string name;
        public int vote;
        public bool enable;
        public int disableDay;
        public bool IsValidForWalking;
        public Place(int id, string name, int vote, bool enable,bool isWalidForWalking,int disableDay)
        {
            this.id = id;
            this.name = name;
            this.vote = vote;
            this.enable = enable;
            this.IsValidForWalking = isWalidForWalking;
            this.disableDay = disableDay;
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
