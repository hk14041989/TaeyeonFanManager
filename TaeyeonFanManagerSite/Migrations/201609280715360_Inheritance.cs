namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            // Drop foreign keys and indexes that point to tables we're going to drop.
            DropForeignKey("dbo.JoinedDate", "FanID", "dbo.Fan");
            DropIndex("dbo.JoinedDate", new[] { "FanID" });

            RenameTable(name: "dbo.Idol", newName: "Person");
            AddColumn("dbo.Person", "JoinedDate", c => c.DateTime());
            AddColumn("dbo.Person", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Idol"));
            AlterColumn("dbo.Person", "MeetDate", c => c.DateTime());
            AddColumn("dbo.Person", "OldId", c => c.Int(nullable: true));

            // Copy existing Student data into new Person table.
            Sql("INSERT INTO dbo.Person (LastName, FirstName, MeetDate, JoinedDate, Discriminator, OldId) SELECT LastName, FirstName, null AS MeetName, JoinedDate, 'Fan' AS Discriminator, ID AS OldId FROM dbo.Fan");

            // Fix up existing relationships to match new PK's.
            Sql("UPDATE dbo.JoinedDate SET FanID = (SELECT ID FROM dbo.Person WHERE OldId = JoinedDate.FanID AND Discriminator = 'Fan')");

            // Remove temporary key
            DropColumn("dbo.Person", "OldId");

            DropTable("dbo.Fan");

            // Re-create foreign keys and indexes pointing to new table.
            AddForeignKey("dbo.JoinedDate", "FanID", "dbo.Person", "ID", cascadeDelete: true);
            CreateIndex("dbo.JoinedDate", "FanID");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        JoinedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Person", "MeetDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Person", "Discriminator");
            DropColumn("dbo.Person", "JoinedDate");
            RenameTable(name: "dbo.Person", newName: "Idol");
        }
    }
}
