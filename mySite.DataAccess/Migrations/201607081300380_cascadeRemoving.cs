namespace mySite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeRemoving : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Marks", "CriterionId", "dbo.Criteria");
            DropForeignKey("dbo.Marks", "PointId", "dbo.Points");
            AddForeignKey("dbo.Points", "BoardId", "dbo.Boards", cascadeDelete: false);
            AddForeignKey("dbo.Marks", "CriterionId", "dbo.Criteria", cascadeDelete: true);
            AddForeignKey("dbo.Marks", "PointId", "dbo.Points", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Marks", "CriterionId", "dbo.Criteria");
            DropForeignKey("dbo.Marks", "PointId", "dbo.Points");
            AddForeignKey("dbo.Points", "BoardId", "dbo.Boards", cascadeDelete: true);
            AddForeignKey("dbo.Marks", "CriterionId", "dbo.Criteria", cascadeDelete: false);
            AddForeignKey("dbo.Marks", "PointId", "dbo.Points", cascadeDelete: false);
        }
    }
}
