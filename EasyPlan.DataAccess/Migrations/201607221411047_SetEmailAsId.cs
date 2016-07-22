namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetEmailAsId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "UserId" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Roles", "User_Email", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.Users", "Email");
            CreateIndex("dbo.Roles", "User_Email");
            AddForeignKey("dbo.Roles", "User_Email", "dbo.Users", "Email", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "User_Email", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "User_Email" });
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Roles", "User_Email");
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Roles", "UserId");
            AddForeignKey("dbo.Roles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
