namespace IFT604Projet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAvatarFromProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "Avatar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "Avatar", c => c.Binary(storeType: "image"));
        }
    }
}
