namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegionAndInProgressFieldsToGameEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameEvents", "InProgress", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameEvents", "RegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameEvents", "RegionId");
            AddForeignKey("dbo.GameEvents", "RegionId", "dbo.Regions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameEvents", "RegionId", "dbo.Regions");
            DropIndex("dbo.GameEvents", new[] { "RegionId" });
            DropColumn("dbo.GameEvents", "RegionId");
            DropColumn("dbo.GameEvents", "InProgress");
        }
    }
}
