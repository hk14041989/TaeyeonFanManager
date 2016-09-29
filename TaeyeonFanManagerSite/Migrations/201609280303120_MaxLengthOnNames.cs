namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fan", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Fan", "FirstMidName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fan", "FirstMidName", c => c.String());
            AlterColumn("dbo.Fan", "LastName", c => c.String());
        }
    }
}
