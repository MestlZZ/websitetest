namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setMaxLengthForTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Criteria", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Items", "Title", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Title", c => c.String(nullable: false, maxLength: 254));
            AlterColumn("dbo.Criteria", "Title", c => c.String(nullable: false, maxLength: 254));
        }
    }
}
