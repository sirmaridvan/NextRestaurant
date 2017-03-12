using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NeYesekApp.Models
{
    public class Restaurant
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public bool IsValidForWalking { get; set; }

        public bool IsOpen { get; set; }
        
        [Index]
        public double Score { get; set; }

        public string PictureUrl { get; set; }

        [InverseProperty("Restaurant")]
        public virtual ICollection<UserVote> Votes { get; set; } 
    }
}