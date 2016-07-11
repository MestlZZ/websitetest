namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Criteria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Width = c.Int(nullable: false),
                        IsBenefit = c.Boolean(nullable: false),
                        BoardId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId);

            CreateTable(
               "dbo.Points",
               c => new
               {
                   Id = c.Guid(nullable: false),
                   Title = c.String(),
                   BoardId = c.Guid(nullable: false),
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
               .Index(t => t.BoardId);

            CreateTable(
                "dbo.Marks",
                c => new
                {
                    CriterionId = c.Guid(nullable: false),
                    PointId = c.Guid(nullable: false),
                    Value = c.Int(nullable: false),
                    Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.CriterionId, t.PointId })
                .ForeignKey("dbo.Points", t => t.PointId, cascadeDelete: false)
                .ForeignKey("dbo.Criteria", t => t.CriterionId, cascadeDelete: false)
                .Index(t => t.CriterionId)
                .Index(t => t.PointId);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "CriterionId", "dbo.Criteria");
            DropForeignKey("dbo.Marks", "PointId", "dbo.Points");
            DropForeignKey("dbo.Points", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Criteria", "BoardId", "dbo.Boards");
            DropIndex("dbo.Marks", new[] { "CriterionId" });
            DropIndex("dbo.Marks", new[] { "PointId" });
            DropIndex("dbo.Points", new[] { "BoardId" });
            DropIndex("dbo.Criteria", new[] { "BoardId" });
            DropTable("dbo.Points");
            DropTable("dbo.Marks");
            DropTable("dbo.Criteria");
            DropTable("dbo.Boards");
        }
    }
}
