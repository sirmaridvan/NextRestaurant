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
        public int Id { get; set; }
        public double Vote { get; set; }
        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}