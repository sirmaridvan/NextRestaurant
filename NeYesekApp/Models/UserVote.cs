using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NeYesekApp.Models
{
    public class UserVote
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public double Vote { get; set; }
        
        public User User { get; set; }


        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}