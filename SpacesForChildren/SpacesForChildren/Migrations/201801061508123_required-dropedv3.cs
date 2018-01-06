namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddropedv3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Activities", new[] { "InstitutionId" });
            DropIndex("dbo.Contract", new[] { "ParentId" });
            DropIndex("dbo.Review", new[] { "InstitutionId" });
            AlterColumn("dbo.Activities", "InstitutionId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Contract", "ParentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Review", "InstitutionId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Activities", "InstitutionId");
            CreateIndex("dbo.Contract", "ParentId");
            CreateIndex("dbo.Review", "InstitutionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Review", new[] { "InstitutionId" });
            DropIndex("dbo.Contract", new[] { "ParentId" });
            DropIndex("dbo.Activities", new[] { "InstitutionId" });
            AlterColumn("dbo.Review", "InstitutionId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contract", "ParentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Activities", "InstitutionId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Review", "InstitutionId");
            CreateIndex("dbo.Contract", "ParentId");
            CreateIndex("dbo.Activities", "InstitutionId");
        }
    }
}
