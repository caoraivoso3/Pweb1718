namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatanotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Activities", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Institution", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.RequestInfoes", "Title", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.RequestInfoes", "Text", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.RequestInfoes", "Answer", c => c.String(maxLength: 200));
            AlterColumn("dbo.Review", "Title", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Review", "Text", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Services", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Services", "Description", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Services", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Review", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Review", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.RequestInfoes", "Answer", c => c.String());
            AlterColumn("dbo.RequestInfoes", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.RequestInfoes", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Institution", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Activities", "Description", c => c.String());
            AlterColumn("dbo.Activities", "Name", c => c.String());
            DropColumn("dbo.Activities", "Date");
        }
    }
}
