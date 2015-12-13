namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAvatars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
    }
}
