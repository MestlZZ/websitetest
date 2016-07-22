namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleRename : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Id" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "BoardId" });
            DropPrimaryKey("dbo.Roles");
            AddColumn("dbo.Roles", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Roles", "BoardId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Roles", new[] { "UserId", "BoardId" });
            CreateIndex("dbo.Roles", "UserId");
            CreateIndex("dbo.Roles", "BoardId");
            AddForeignKey("dbo.Roles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "LastLogIn");
            DropTable("dbo.UserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        BoardId = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BoardId });
            
            AddColumn("dbo.Users", "LastLogIn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Roles", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "BoardId" });
            DropIndex("dbo.Roles", new[] { "UserId" });
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Roles", "BoardId");
            DropColumn("dbo.Roles", "UserId");
            AddPrimaryKey("dbo.Roles", "Id");
            CreateIndex("dbo.UserRoles", "BoardId");
            CreateIndex("dbo.UserRoles", "UserId");
            CreateIndex("dbo.UserRoles", "Id");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "Id", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
