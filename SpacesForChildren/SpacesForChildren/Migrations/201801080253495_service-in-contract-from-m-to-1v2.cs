namespace SpacesForChildren.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviceincontractfrommto1v2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Contract", name: "SeviceId", newName: "ServiceId");
            RenameIndex(table: "dbo.Contract", name: "IX_SeviceId", newName: "IX_ServiceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Contract", name: "IX_ServiceId", newName: "IX_SeviceId");
            RenameColumn(table: "dbo.Contract", name: "ServiceId", newName: "SeviceId");
        }
    }
}
