namespace Service.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToPendingPasswordResetRelationship : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.PendingPasswordResets", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            CreateIndex("dbo.PendingPasswordResets", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PendingPasswordResets", new[] { "UserId" });
            DropForeignKey("dbo.PendingPasswordResets", "UserId", "dbo.Users");
        }
    }
}
