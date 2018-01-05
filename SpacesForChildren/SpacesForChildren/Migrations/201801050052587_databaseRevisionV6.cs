namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseRevisionV6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Institution", "LocalId", "dbo.Local");
            DropIndex("dbo.Institution", new[] { "LocalId" });
            AlterColumn("dbo.Parent", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Children", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Institution", "LocalId");
            DropTable("dbo.Local");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Local",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Institution", "LocalId", c => c.Int(nullable: false));
            AlterColumn("dbo.Children", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Parent", "Gender", c => c.String(nullable: false));
            CreateIndex("dbo.Institution", "LocalId");
            AddForeignKey("dbo.Institution", "LocalId", "dbo.Local", "Id", cascadeDelete: true);
        }
    }
}
