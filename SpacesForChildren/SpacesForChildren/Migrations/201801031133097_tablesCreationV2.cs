namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablesCreationV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        ParentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parent", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitialDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        ParentId = c.String(nullable: false, maxLength: 128),
                        ChildId = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: true)
                .ForeignKey("dbo.Parent", t => t.ParentId)
                .ForeignKey("dbo.Review", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.ReviewId);
            /*
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FiscalId = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            */
            CreateTable(
                "dbo.RequestInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        IsAnswered = c.Boolean(nullable: false),
                        Answer = c.String(),
                        ParentId = c.String(nullable: false, maxLength: 128),
                        InstitutionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .ForeignKey("dbo.Parent", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.InstitutionId);
            
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
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Ranking = c.Int(nullable: false),
                        InstitutionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MinAgeYear = c.Int(nullable: false),
                        MaxAgeYear = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        InstitutionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.ServiceContracts",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Contract_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Contract_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Contract", t => t.Contract_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.Institution",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Acronym = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        LocalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Local", t => t.LocalId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.LocalId);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            //DropTable("dbo.AspNetUsers");
        }
        
        public override void Down()
        {
            /*
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FiscalId = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            */
            DropForeignKey("dbo.Parent", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Institution", "LocalId", "dbo.Local");
            DropForeignKey("dbo.Institution", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Children", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.Contract", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.Contract", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.RequestInfoes", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.RequestInfoes", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Services", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.ServiceContracts", "Contract_Id", "dbo.Contract");
            DropForeignKey("dbo.ServiceContracts", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Review", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Contract", "ChildId", "dbo.Children");
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.Institution", new[] { "LocalId" });
            DropIndex("dbo.Institution", new[] { "Id" });
            DropIndex("dbo.ServiceContracts", new[] { "Contract_Id" });
            DropIndex("dbo.ServiceContracts", new[] { "Service_Id" });
            DropIndex("dbo.Services", new[] { "InstitutionId" });
            DropIndex("dbo.Review", new[] { "InstitutionId" });
            DropIndex("dbo.RequestInfoes", new[] { "InstitutionId" });
            DropIndex("dbo.RequestInfoes", new[] { "ParentId" });
            DropIndex("dbo.Contract", new[] { "ReviewId" });
            DropIndex("dbo.Contract", new[] { "ChildId" });
            DropIndex("dbo.Contract", new[] { "ParentId" });
            DropIndex("dbo.Children", new[] { "ParentId" });
            DropTable("dbo.Parent");
            DropTable("dbo.Institution");
            DropTable("dbo.ServiceContracts");
            DropTable("dbo.Services");
            DropTable("dbo.Review");
            DropTable("dbo.Local");
            DropTable("dbo.RequestInfoes");
            //DropTable("dbo.AspNetUsers");
            DropTable("dbo.Contract");
            DropTable("dbo.Children");
        }
    }
}
