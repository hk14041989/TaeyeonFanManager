namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        JoinedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JoinedDate",
                c => new
                    {
                        JoinedDateID = c.Int(nullable: false, identity: true),
                        OfflineID = c.Int(nullable: false),
                        FanID = c.Int(nullable: false),
                        Rank = c.Int(),
                    })
                .PrimaryKey(t => t.JoinedDateID)
                .ForeignKey("dbo.Fan", t => t.FanID, cascadeDelete: true)
                .ForeignKey("dbo.Offline", t => t.OfflineID, cascadeDelete: true)
                .Index(t => t.OfflineID)
                .Index(t => t.FanID);
            
            CreateTable(
                "dbo.Offline",
                c => new
                    {
                        OfflineID = c.Int(nullable: false),
                        Title = c.String(),
                        TicketPrices = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OfflineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JoinedDate", "OfflineID", "dbo.Offline");
            DropForeignKey("dbo.JoinedDate", "FanID", "dbo.Fan");
            DropIndex("dbo.JoinedDate", new[] { "FanID" });
            DropIndex("dbo.JoinedDate", new[] { "OfflineID" });
            DropTable("dbo.Offline");
            DropTable("dbo.JoinedDate");
            DropTable("dbo.Fan");
        }
    }
}
