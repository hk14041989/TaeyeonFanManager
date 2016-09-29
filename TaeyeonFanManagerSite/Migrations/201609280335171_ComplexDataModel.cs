namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concept",
                c => new
                    {
                        ConceptID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        IdolID = c.Int(),
                    })
                .PrimaryKey(t => t.ConceptID)
                .ForeignKey("dbo.Idol", t => t.IdolID)
                .Index(t => t.IdolID);
            
            CreateTable(
                "dbo.Idol",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MeetDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FanSign",
                c => new
                    {
                        IdolID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdolID)
                .ForeignKey("dbo.Idol", t => t.IdolID)
                .Index(t => t.IdolID);
            
            CreateTable(
                "dbo.OfflineIdol",
                c => new
                    {
                        OfflineId = c.Int(nullable: false),
                        IdolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OfflineId, t.IdolID })
                .ForeignKey("dbo.Offline", t => t.OfflineId, cascadeDelete: true)
                .ForeignKey("dbo.Idol", t => t.IdolID, cascadeDelete: true)
                .Index(t => t.OfflineId)
                .Index(t => t.IdolID);

            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Concept (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            //  default value for FK points to department created above.
            AddColumn("dbo.Offline", "ConceptID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Offline", "ConceptID", c => c.Int(nullable: false));

            AlterColumn("dbo.Fan", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Fan", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Offline", "Title", c => c.String(maxLength: 50));
            CreateIndex("dbo.Offline", "ConceptID");
            AddForeignKey("dbo.Offline", "ConceptID", "dbo.Concept", "ConceptID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concept", "IdolID", "dbo.Idol");
            DropForeignKey("dbo.OfflineIdol", "IdolID", "dbo.Idol");
            DropForeignKey("dbo.OfflineIdol", "OfflineId", "dbo.Offline");
            DropForeignKey("dbo.Offline", "ConceptID", "dbo.Concept");
            DropForeignKey("dbo.FanSign", "IdolID", "dbo.Idol");
            DropIndex("dbo.OfflineIdol", new[] { "IdolID" });
            DropIndex("dbo.OfflineIdol", new[] { "OfflineId" });
            DropIndex("dbo.Offline", new[] { "ConceptID" });
            DropIndex("dbo.FanSign", new[] { "IdolID" });
            DropIndex("dbo.Concept", new[] { "IdolID" });
            AlterColumn("dbo.Offline", "Title", c => c.String());
            AlterColumn("dbo.Fan", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Fan", "LastName", c => c.String(maxLength: 50));
            DropColumn("dbo.Offline", "ConceptID");
            DropTable("dbo.OfflineIdol");
            DropTable("dbo.FanSign");
            DropTable("dbo.Idol");
            DropTable("dbo.Concept");
        }
    }
}
