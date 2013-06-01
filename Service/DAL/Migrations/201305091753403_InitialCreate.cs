using System.Data.Entity.Migrations;

namespace Service.DAL.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskDescription = c.String(),
                        TaskStatus = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PendingUsers",
                c => new
                    {
                        PendingUserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Secret = c.String(),
                    })
                .PrimaryKey(t => t.PendingUserId);
            
            CreateTable(
                "dbo.PendingPasswordResets",
                c => new
                    {
                        PendingPasswordResetId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Secret = c.String(),
                    })
                .PrimaryKey(t => t.PendingPasswordResetId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users");
            DropTable("dbo.PendingPasswordResets");
            DropTable("dbo.PendingUsers");
            DropTable("dbo.Tasks");
            DropTable("dbo.Sessions");
            DropTable("dbo.Users");
        }
    }
}
