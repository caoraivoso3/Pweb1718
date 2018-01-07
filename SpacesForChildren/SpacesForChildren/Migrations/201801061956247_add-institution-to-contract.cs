namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinstitutiontocontract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "Institution_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Contract", "InstitutionId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Children", "Institution_Id");
            CreateIndex("dbo.Contract", "InstitutionId");
            AddForeignKey("dbo.Contract", "InstitutionId", "dbo.Institution", "Id");
            AddForeignKey("dbo.Children", "Institution_Id", "dbo.Institution", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Children", "Institution_Id", "dbo.Institution");
            DropForeignKey("dbo.Contract", "InstitutionId", "dbo.Institution");
            DropIndex("dbo.Contract", new[] { "InstitutionId" });
            DropIndex("dbo.Children", new[] { "Institution_Id" });
            DropColumn("dbo.Contract", "InstitutionId");
            DropColumn("dbo.Children", "Institution_Id");
        }
    }
}
