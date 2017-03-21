using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NeYesekApp.Models
{
    [Table("RestaurantHistories")]
    public class RestaurantHistory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RestaurantName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }
    }
}