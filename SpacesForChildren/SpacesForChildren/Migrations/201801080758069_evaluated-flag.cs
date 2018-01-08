namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class evaluatedflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contract", "Evaluated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contract", "Evaluated");
        }
    }
}
