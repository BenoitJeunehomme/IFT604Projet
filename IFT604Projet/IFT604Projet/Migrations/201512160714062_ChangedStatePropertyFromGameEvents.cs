namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedStatePropertyFromGameEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameEvents", "State", c => c.Int(nullable: false));
            DropColumn("dbo.GameEvents", "InProgress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameEvents", "InProgress", c => c.Boolean(nullable: false));
            DropColumn("dbo.GameEvents", "State");
        }
    }
}
