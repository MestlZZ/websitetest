namespace EasyPlan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleToRights : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Roles", newName: "Rights");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Rights", newName: "Roles");
        }
    }
}
