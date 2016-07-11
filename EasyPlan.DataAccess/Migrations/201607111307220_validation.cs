namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Boards", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Criteria", "Title", c => c.String(nullable: false, maxLength: 254));
            AlterColumn("dbo.Items", "Title", c => c.String(nullable: false, maxLength: 254));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Title", c => c.String());
            AlterColumn("dbo.Criteria", "Title", c => c.String());
            AlterColumn("dbo.Boards", "Title", c => c.String());
        }
    }
}
