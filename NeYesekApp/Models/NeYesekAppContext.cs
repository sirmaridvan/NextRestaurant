using NeYesekApp.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NeYesekApp.Models
{
    public class NeYesekAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public NeYesekAppContext() : base("name=NeYesekAppContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NeYesekAppContext, Configuration>());
        }

        public System.Data.Entity.DbSet<NeYesekApp.Models.Restaurant> Restaurants { get; set; }
        public System.Data.Entity.DbSet<NeYesekApp.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<NeYesekApp.Models.UserVote> UserVotes { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany<UserVote>(u => u.Votes)
        //        .WithRequired(v => v.User);

        //    modelBuilder.Entity<Restaurant>()
        //        .HasMany<UserVote>(u => u.Votes)
        //        .WithRequired(v => v.Restaurant);
        //}
    }
}
