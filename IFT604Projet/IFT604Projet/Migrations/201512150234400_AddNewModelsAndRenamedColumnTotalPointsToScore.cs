namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewModelsAndRenamedColumnTotalPointsToScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bombs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDefused = c.Boolean(nullable: false),
                        GameId = c.Int(nullable: false),
                        PlantedForGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameEvents", t => t.PlantedForGame_Id)
                .Index(t => t.PlantedForGame_Id);
            
            CreateTable(
                "dbo.GameEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartPlacing = c.DateTime(nullable: false),
                        EndPlacing = c.DateTime(nullable: false),
                        StartDefusing = c.DateTime(nullable: false),
                        StopDefusing = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Radius = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "RegionId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "GameEvent_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "GameEvent_Id1", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RegionId");
            CreateIndex("dbo.AspNetUsers", "GameEvent_Id");
            CreateIndex("dbo.AspNetUsers", "GameEvent_Id1");
            AddForeignKey("dbo.AspNetUsers", "RegionId", "dbo.Regions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "GameEvent_Id", "dbo.GameEvents", "Id");
            AddForeignKey("dbo.AspNetUsers", "GameEvent_Id1", "dbo.GameEvents", "Id");
            DropColumn("dbo.AspNetUsers", "Region");
            DropColumn("dbo.AspNetUsers", "TotalPoints");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TotalPoints", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Region", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "GameEvent_Id1", "dbo.GameEvents");
            DropForeignKey("dbo.AspNetUsers", "GameEvent_Id", "dbo.GameEvents");
            DropForeignKey("dbo.AspNetUsers", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.GameEvents", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.GameEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bombs", "PlantedForGame_Id", "dbo.GameEvents");
            DropIndex("dbo.AspNetUsers", new[] { "GameEvent_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "GameEvent_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "RegionId" });
            DropIndex("dbo.GameEvents", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.GameEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Bombs", new[] { "PlantedForGame_Id" });
            DropColumn("dbo.AspNetUsers", "GameEvent_Id1");
            DropColumn("dbo.AspNetUsers", "GameEvent_Id");
            DropColumn("dbo.AspNetUsers", "Score");
            DropColumn("dbo.AspNetUsers", "RegionId");
            DropTable("dbo.Regions");
            DropTable("dbo.GameEvents");
            DropTable("dbo.Bombs");
        }
    }
}
