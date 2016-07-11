namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Points", newName: "Items");
            RenameColumn(table: "dbo.Marks", name: "PointId", newName: "ItemId");
            RenameIndex(table: "dbo.Marks", name: "IX_PointId", newName: "IX_ItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Marks", name: "IX_ItemId", newName: "IX_PointId");
            RenameColumn(table: "dbo.Marks", name: "ItemId", newName: "PointId");
            RenameTable(name: "dbo.Items", newName: "Points");
        }
    }
}
