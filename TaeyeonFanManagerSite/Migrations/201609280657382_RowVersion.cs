namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concept", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterStoredProcedure(
                "dbo.Concept_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        IdolID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Concept]([Name], [Budget], [StartDate], [IdolID])
                      VALUES (@Name, @Budget, @StartDate, @IdolID)
                      
                      DECLARE @ConceptID int
                      SELECT @ConceptID = [ConceptID]
                      FROM [dbo].[Concept]
                      WHERE @@ROWCOUNT > 0 AND [ConceptID] = scope_identity()
                      
                      SELECT t0.[ConceptID], t0.[RowVersion]
                      FROM [dbo].[Concept] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ConceptID] = @ConceptID"
            );
            
            AlterStoredProcedure(
                "dbo.Concept_Update",
                p => new
                    {
                        ConceptID = p.Int(),
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        IdolID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Concept]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [IdolID] = @IdolID
                      WHERE (([ConceptID] = @ConceptID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Concept] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ConceptID] = @ConceptID"
            );
            
            AlterStoredProcedure(
                "dbo.Concept_Delete",
                p => new
                    {
                        ConceptID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Concept]
                      WHERE (([ConceptID] = @ConceptID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Concept", "RowVersion");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
