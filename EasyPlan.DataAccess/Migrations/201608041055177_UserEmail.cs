namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEmail : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Roles");
            DropIndex("dbo.Roles", new[] { "User_Email" });
            DropColumn("dbo.Roles", "UserId");
            RenameColumn(table: "dbo.Roles", name: "User_Email", newName: "UserId");
            AlterColumn("dbo.Roles", "UserId", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.Roles", new[] { "UserId", "BoardId" });
            CreateIndex("dbo.Roles", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Roles");
            DropIndex("dbo.Roles", new[] { "UserId" });
            AlterColumn("dbo.Roles", "UserId", c => c.Guid(nullable: false));            
            RenameColumn(table: "dbo.Roles", name: "UserId", newName: "User_Email");
            AddColumn("dbo.Roles", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Roles", "User_Email");
            AddPrimaryKey("dbo.Roles", new[] { "UserId", "BoardId" });
        }
    }
}
