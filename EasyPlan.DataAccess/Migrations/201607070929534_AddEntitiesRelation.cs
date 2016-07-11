namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntitiesRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Criteria", name: "BoardId", newName: "Board_Id");
            RenameColumn(table: "dbo.Points", name: "BoardId", newName: "Board_Id");
            RenameColumn(table: "dbo.Marks", name: "PointId", newName: "Point_Id");
            RenameColumn(table: "dbo.Marks", name: "CriterionId", newName: "Criterion_Id");
            DropPrimaryKey("dbo.Marks");
            AddPrimaryKey("dbo.Marks", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Marks");
            AddPrimaryKey("dbo.Marks", new[] { "CriterionId", "PointId" });
            RenameColumn(table: "dbo.Marks", name: "Criterion_Id", newName: "CriterionId");
            RenameColumn(table: "dbo.Marks", name: "Point_Id", newName: "PointId");
            RenameColumn(table: "dbo.Points", name: "Board_Id", newName: "BoardId");
            RenameColumn(table: "dbo.Criteria", name: "Board_Id", newName: "BoardId");
        }
    }
}
