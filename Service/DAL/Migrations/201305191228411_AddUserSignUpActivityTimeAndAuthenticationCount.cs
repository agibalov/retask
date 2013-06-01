namespace Service.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSignUpActivityTimeAndAuthenticationCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AuthenticationCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RegisteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "LastActivityAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastActivityAt");
            DropColumn("dbo.Users", "RegisteredAt");
            DropColumn("dbo.Users", "AuthenticationCount");
        }
    }
}
