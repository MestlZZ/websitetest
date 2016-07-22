namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userauth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 30),
                    HashPassword = c.String(nullable: false, maxLength: 255),
                    Email = c.String(nullable: false, maxLength: 255),
                    LastLogIn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        BoardId = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BoardId })
                .ForeignKey("dbo.Roles", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BoardId)
                .Index(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Id", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "Id" });
            DropIndex("dbo.UserRoles", new[] { "BoardId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
