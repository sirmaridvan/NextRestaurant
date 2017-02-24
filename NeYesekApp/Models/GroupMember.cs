using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeYesekApp.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Category { get; set; }
        public Double Price { get; set; }

        public GroupMember() { }

        public GroupMember(int _Id, string _name, string _category, double _price)
        {
            this.Id = _Id;
            this.Name = _name;
            this.Category = _category;
            this.Price = _price;
        }
    }
}