namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviceincontractfrommto1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceContracts", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceContracts", "Contract_Id", "dbo.Contract");
            DropIndex("dbo.ServiceContracts", new[] { "Service_Id" });
            DropIndex("dbo.ServiceContracts", new[] { "Contract_Id" });
            AddColumn("dbo.Contract", "SeviceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contract", "SeviceId");
            AddForeignKey("dbo.Contract", "SeviceId", "dbo.Services", "Id", cascadeDelete: true);
            DropTable("dbo.ServiceContracts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceContracts",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Contract_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Contract_Id });
            
            DropForeignKey("dbo.Contract", "SeviceId", "dbo.Services");
            DropIndex("dbo.Contract", new[] { "SeviceId" });
            DropColumn("dbo.Contract", "SeviceId");
            CreateIndex("dbo.ServiceContracts", "Contract_Id");
            CreateIndex("dbo.ServiceContracts", "Service_Id");
            AddForeignKey("dbo.ServiceContracts", "Contract_Id", "dbo.Contract", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceContracts", "Service_Id", "dbo.Services", "Id", cascadeDelete: true);
        }
    }
}
