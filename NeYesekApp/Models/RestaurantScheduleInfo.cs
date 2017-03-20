using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace NeYesekApp.Models
{
    public class RestaurantScheduleInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Possibility { get; set; }

        public bool Enable { get; set; }

        public int DisableDay { get; set; } 
        public virtual Restaurant Restaurant { get; set; }
    }
}