namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contracteapprovationenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contract", "Approvation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contract", "Approvation");
        }
    }
}
