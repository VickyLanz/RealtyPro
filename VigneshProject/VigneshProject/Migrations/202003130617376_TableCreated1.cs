namespace VigneshProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableCreated1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropID = c.Int(nullable: false, identity: true),
                        PropName = c.String(),
                        Location = c.String(),
                        PropOwnerName = c.String(),
                        PropDescription = c.String(),
                        PropType = c.String(),
                        PhoneNo = c.Long(nullable: false),
                        PropImage = c.String(),
                        UserID = c.Int(nullable: false),
                        propertyType_TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.PropID)
                .ForeignKey("dbo.PropertyTypes", t => t.propertyType_TypeID)
                .Index(t => t.propertyType_TypeID);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        PropType = c.String(),
                    })
                .PrimaryKey(t => t.TypeID);
            Sql("Insert into PropertyTypes values('Rental')");
            Sql("Insert into PropertyTypes values('Buy')");
            Sql("Insert into PropertyTypes values('Industrial Space')");


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "propertyType_TypeID", "dbo.PropertyTypes");
            DropIndex("dbo.Properties", new[] { "propertyType_TypeID" });
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.Properties");
        }
    }
}
