namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractreviewidsettointquestionmark : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contract", "ReviewId", "dbo.Review");
            DropIndex("dbo.Contract", new[] { "ReviewId" });
            AlterColumn("dbo.Contract", "ReviewId", c => c.Int());
            CreateIndex("dbo.Contract", "ReviewId");
            AddForeignKey("dbo.Contract", "ReviewId", "dbo.Review", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contract", "ReviewId", "dbo.Review");
            DropIndex("dbo.Contract", new[] { "ReviewId" });
            AlterColumn("dbo.Contract", "ReviewId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contract", "ReviewId");
            AddForeignKey("dbo.Contract", "ReviewId", "dbo.Review", "Id", cascadeDelete: true);
        }
    }
}
