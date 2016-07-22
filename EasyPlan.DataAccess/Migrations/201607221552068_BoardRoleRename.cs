namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardRoleRename : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Roles", "BoardId", "dbo.Boards", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "BoardId", "dbo.Boards");
        }
    }
}
