namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddroped : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RequestInfoes", new[] { "ParentId" });
            AlterColumn("dbo.RequestInfoes", "ParentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.RequestInfoes", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestInfoes", new[] { "ParentId" });
            AlterColumn("dbo.RequestInfoes", "ParentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.RequestInfoes", "ParentId");
        }
    }
}
