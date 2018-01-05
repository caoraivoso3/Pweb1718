namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUpdatev7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Institution", "Acronym");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institution", "Acronym", c => c.String(nullable: false));
        }
    }
}
