namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractapprovation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contract", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contract", "IsApproved", c => c.Boolean(nullable: false));
        }
    }
}
