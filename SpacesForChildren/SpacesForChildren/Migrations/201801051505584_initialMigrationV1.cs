namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigrationV1 : DbMigration 
    {
        public override void Up()
        {
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        NIF = c.Int(nullable: false),
                        Address = c.String(),
                        City = c.String(),
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                "dbo.Children",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.Institution",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parent", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Institution", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Activities", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.RequestInfoes", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.Children", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.ServiceInstitutions", "Institution_Id", "dbo.Institution");
            DropForeignKey("dbo.ServiceInstitutions", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceContracts", "Contract_Id", "dbo.Contract");
            DropForeignKey("dbo.ServiceContracts", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Contract", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.Review", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Contract", "ParentId", "dbo.Parent");
            DropForeignKey("dbo.Contract", "ChildId", "dbo.Children");
            DropForeignKey("dbo.RequestInfoes", "InstitutionId", "dbo.Institution");
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.Institution", new[] { "Id" });
            DropIndex("dbo.ServiceInstitutions", new[] { "Institution_Id" });
            DropIndex("dbo.ServiceInstitutions", new[] { "Service_Id" });
            DropIndex("dbo.ServiceContracts", new[] { "Contract_Id" });
            DropIndex("dbo.ServiceContracts", new[] { "Service_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Review", new[] { "InstitutionId" });
            DropIndex("dbo.Contract", new[] { "ReviewId" });
            DropIndex("dbo.Contract", new[] { "ChildId" });
            DropIndex("dbo.Contract", new[] { "ParentId" });
            DropIndex("dbo.Children", new[] { "ParentId" });
            DropIndex("dbo.RequestInfoes", new[] { "InstitutionId" });
            DropIndex("dbo.RequestInfoes", new[] { "ParentId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Activities", new[] { "InstitutionId" });
            DropTable("dbo.Parent");
            DropTable("dbo.Institution");
            DropTable("dbo.ServiceInstitutions");
            DropTable("dbo.ServiceContracts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Services");
            DropTable("dbo.Review");
            DropTable("dbo.Contract");
            DropTable("dbo.Children");
            DropTable("dbo.RequestInfoes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Activities");
        }
    }
}
