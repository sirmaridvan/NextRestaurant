using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NeYesekApp.Models
{
    public class User
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(30)]
        public string Email { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Hash { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserVote> Votes { get; set; }
    }
}