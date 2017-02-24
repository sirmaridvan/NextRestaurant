namespace NeYesekApp.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NeYesekApp.Models.NeYesekAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NeYesekApp.Models.NeYesekAppContext context)
        {
            context.GroupMembers.AddOrUpdate(x => x.Id,
                new Models.GroupMember(5, "Elif Benli", "Owner", 4.5),
                new Models.GroupMember(5, "İlkan Engin", "Owner", 1.5),
                new Models.GroupMember(5, "Rıdvan Sırma", "Owner", 6.5));
            context.SaveChanges();
        }
    }
}
