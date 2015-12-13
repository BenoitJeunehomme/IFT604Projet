namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectFieldsToUserProfiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Region", c => c.String());
            AddColumn("dbo.AspNetUsers", "TotalPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalPoints");
            DropColumn("dbo.AspNetUsers", "Region");
        }
    }
}
