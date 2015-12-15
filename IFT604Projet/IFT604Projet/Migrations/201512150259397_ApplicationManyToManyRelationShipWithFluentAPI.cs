namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationManyToManyRelationShipWithFluentAPI : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bombs", "PlantedForGame_Id", "dbo.GameEvents");
            RenameColumn(table: "dbo.Bombs", name: "PlantedForGame_Id", newName: "PlantedForGame_GameEventId");
            RenameIndex(table: "dbo.Bombs", name: "IX_PlantedForGame_Id", newName: "IX_PlantedForGame_GameEventId");
            DropPrimaryKey("dbo.GameEvents");
            DropColumn("dbo.GameEvents", "Id");

            AddColumn("dbo.GameEvents", "GameEventId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GameEvents", "GameEventId");
            AddForeignKey("dbo.Bombs", "PlantedForGame_GameEventId", "dbo.GameEvents", "GameEventId");

            CreateTable(
                "dbo.DefusersGameEvent",
                c => new
                    {
                        GameEventId = c.Int(nullable: false),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GameEventId, t.Id })
                .ForeignKey("dbo.GameEvents", t => t.GameEventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.GameEventId)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PlacersGameEvent",
                c => new
                    {
                        GameEventId = c.Int(nullable: false),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GameEventId, t.Id })
                .ForeignKey("dbo.GameEvents", t => t.GameEventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.GameEventId)
                .Index(t => t.Id);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameEvents", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Bombs", "PlantedForGame_GameEventId", "dbo.GameEvents");
            DropForeignKey("dbo.PlacersGameEvent", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlacersGameEvent", "GameEventId", "dbo.GameEvents");
            DropForeignKey("dbo.DefusersGameEvent", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DefusersGameEvent", "GameEventId", "dbo.GameEvents");
            DropIndex("dbo.PlacersGameEvent", new[] { "Id" });
            DropIndex("dbo.PlacersGameEvent", new[] { "GameEventId" });
            DropIndex("dbo.DefusersGameEvent", new[] { "Id" });
            DropIndex("dbo.DefusersGameEvent", new[] { "GameEventId" });
            DropPrimaryKey("dbo.GameEvents");
            DropColumn("dbo.GameEvents", "GameEventId");
            DropTable("dbo.PlacersGameEvent");
            DropTable("dbo.DefusersGameEvent");
            AddPrimaryKey("dbo.GameEvents", "Id");
            RenameIndex(table: "dbo.Bombs", name: "IX_PlantedForGame_GameEventId", newName: "IX_PlantedForGame_Id");
            RenameColumn(table: "dbo.Bombs", name: "PlantedForGame_GameEventId", newName: "PlantedForGame_Id");
            AddForeignKey("dbo.Bombs", "PlantedForGame_Id", "dbo.GameEvents", "Id");
        }
    }
}
