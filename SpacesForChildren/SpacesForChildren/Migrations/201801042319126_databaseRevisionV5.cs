namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseRevisionV5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "InstitutionId", "dbo.Institution");
            DropIndex("dbo.Services", new[] { "InstitutionId" });
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        InstitutionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.ServiceInstitutions",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Institution_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Institution_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Institution", t => t.Institution_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.Institution_Id);
            
            AddColumn("dbo.AspNetUsers", "NIF", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            DropColumn("dbo.Contract", "TotalPrice");
            DropColumn("dbo.AspNetUsers", "FiscalId");
            DropColumn("dbo.Services", "InstitutionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "InstitutionId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FiscalId", c => c.Int(nullable: false));
            AddColumn("dbo.Contract", "TotalPrice", c => c.Int(nullable: false));
            DropForeignKey("dbo.Activities", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.ServiceInstitutions", "Institution_Id", "dbo.Institution");
            DropForeignKey("dbo.ServiceInstitutions", "Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceInstitutions", new[] { "Institution_Id" });
            DropIndex("dbo.ServiceInstitutions", new[] { "Service_Id" });
            DropIndex("dbo.Activities", new[] { "InstitutionId" });
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "NIF");
            DropTable("dbo.ServiceInstitutions");
            DropTable("dbo.Activities");
            CreateIndex("dbo.Services", "InstitutionId");
            AddForeignKey("dbo.Services", "InstitutionId", "dbo.Institution", "Id");
        }
    }
}
