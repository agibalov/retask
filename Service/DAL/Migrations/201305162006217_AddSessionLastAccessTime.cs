namespace Service.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionLastAccessTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "LastAccessTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "LastAccessTime");
        }
    }
}
