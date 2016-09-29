namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
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
                      
                      SELECT t0.[ConceptID]
                      FROM [dbo].[Concept] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ConceptID] = @ConceptID"
            );
            
            CreateStoredProcedure(
                "dbo.Concept_Update",
                p => new
                    {
                        ConceptID = p.Int(),
                        Name = p.String(maxLength: 50),
                        Budget = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        IdolID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Concept]
                      SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [IdolID] = @IdolID
                      WHERE ([ConceptID] = @ConceptID)"
            );
            
            CreateStoredProcedure(
                "dbo.Concept_Delete",
                p => new
                    {
                        ConceptID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Concept]
                      WHERE ([ConceptID] = @ConceptID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Concept_Delete");
            DropStoredProcedure("dbo.Concept_Update");
            DropStoredProcedure("dbo.Concept_Insert");
        }
    }
}
