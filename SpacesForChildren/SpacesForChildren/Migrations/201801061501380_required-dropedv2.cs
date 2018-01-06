namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddropedv2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RequestInfoes", new[] { "InstitutionId" });
            AlterColumn("dbo.RequestInfoes", "InstitutionId", c => c.String(maxLength: 128));
            CreateIndex("dbo.RequestInfoes", "InstitutionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestInfoes", new[] { "InstitutionId" });
            AlterColumn("dbo.RequestInfoes", "InstitutionId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.RequestInfoes", "InstitutionId");
        }
    }
}
