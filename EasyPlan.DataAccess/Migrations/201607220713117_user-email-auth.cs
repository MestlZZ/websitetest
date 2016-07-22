namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useremailauth : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
