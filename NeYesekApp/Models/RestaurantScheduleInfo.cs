using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace NeYesekApp.Models
{
    [Table("RestaurantScheduleInformations")]
    public class RestaurantScheduleInfo
    {
        [Key]
        public int Id { get; set; }

        public double Possibility { get; set; }

        public bool Enable { get; set; }

        public int DisableDay { get; set; } 

        public virtual Restaurant Restaurant { get; set; }
    }
}