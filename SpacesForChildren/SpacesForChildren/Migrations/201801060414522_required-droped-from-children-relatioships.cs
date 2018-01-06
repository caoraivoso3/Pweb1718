namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddropedfromchildrenrelatioships : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Children", new[] { "ParentId" });
            AlterColumn("dbo.Children", "ParentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Children", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Children", new[] { "ParentId" });
            AlterColumn("dbo.Children", "ParentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Children", "ParentId");
        }
    }
}
