namespace VigneshProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableCreated2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "UserID", c => c.Int(nullable: false));
        }
    }
}
