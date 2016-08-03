namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedBy_string : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boards", "CreatedBy_Email", "dbo.Users");
            DropIndex("dbo.Boards", new[] { "CreatedBy_Email" });
            AddColumn("dbo.Boards", "CreatedBy", c => c.String());
            DropColumn("dbo.Boards", "CreatedBy_Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Boards", "CreatedBy_Email", c => c.String(maxLength: 255));
            DropColumn("dbo.Boards", "CreatedBy");
            CreateIndex("dbo.Boards", "CreatedBy_Email");
            AddForeignKey("dbo.Boards", "CreatedBy_Email", "dbo.Users", "Email");
        }
    }
}
