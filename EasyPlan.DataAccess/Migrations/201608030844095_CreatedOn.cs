namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Criteria", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Marks", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedOn");
            DropColumn("dbo.Roles", "CreatedOn");
            DropColumn("dbo.Items", "CreatedOn");
            DropColumn("dbo.Marks", "CreatedOn");
            DropColumn("dbo.Criteria", "CreatedOn");
            DropColumn("dbo.Boards", "CreatedOn");
        }
    }
}
