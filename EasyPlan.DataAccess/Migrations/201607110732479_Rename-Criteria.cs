namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCriteria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Criteria", "Weight", c => c.Int(nullable: false));
            DropColumn("dbo.Criteria", "Width");
            /*RenameTable("dbo.Criteria", "Criterions");*/
        }
        
        public override void Down()
        {
            /*RenameTable("dbo.Criterions", "Criteria");*/
            AddColumn("dbo.Criteria", "Width", c => c.Int(nullable: false));
            DropColumn("dbo.Criteria", "Weight");
        }
    }
}
