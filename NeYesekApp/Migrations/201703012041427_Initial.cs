namespace NeYesekApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("UserVotes", "FK_dbo.UserVotes_dbo.UserVotes_UserId");
        }
        
        public override void Down()
        {
        }
    }
}
