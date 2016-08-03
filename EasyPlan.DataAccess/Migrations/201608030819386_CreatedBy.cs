namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "CreatedBy_Email", c => c.String(maxLength: 255));
            CreateIndex("dbo.Boards", "CreatedBy_Email");
            AddForeignKey("dbo.Boards", "CreatedBy_Email", "dbo.Users", "Email");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boards", "CreatedBy_Email", "dbo.Users");
            DropIndex("dbo.Boards", new[] { "CreatedBy_Email" });
            DropColumn("dbo.Boards", "CreatedBy_Email");
        }
    }
}
