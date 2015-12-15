namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GameEvents", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "GameEvent_Id", "dbo.GameEvents");
            DropForeignKey("dbo.AspNetUsers", "GameEvent_Id1", "dbo.GameEvents");
            DropIndex("dbo.GameEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GameEvents", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "GameEvent_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "GameEvent_Id1" });
            DropColumn("dbo.GameEvents", "ApplicationUser_Id");
            DropColumn("dbo.GameEvents", "ApplicationUser_Id1");
            DropColumn("dbo.AspNetUsers", "GameEvent_Id");
            DropColumn("dbo.AspNetUsers", "GameEvent_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "GameEvent_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "GameEvent_Id", c => c.Int());
            AddColumn("dbo.GameEvents", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.GameEvents", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "GameEvent_Id1");
            CreateIndex("dbo.AspNetUsers", "GameEvent_Id");
            CreateIndex("dbo.GameEvents", "ApplicationUser_Id1");
            CreateIndex("dbo.GameEvents", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "GameEvent_Id1", "dbo.GameEvents", "Id");
            AddForeignKey("dbo.AspNetUsers", "GameEvent_Id", "dbo.GameEvents", "Id");
            AddForeignKey("dbo.GameEvents", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GameEvents", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
