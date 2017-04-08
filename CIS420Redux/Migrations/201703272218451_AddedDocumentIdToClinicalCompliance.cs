namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocumentIdToClinicalCompliance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClincalCompliances", "DocumentId", c => c.Int(nullable: true));
            AlterColumn("dbo.ClincalCompliances", "ExpirationDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClincalCompliances", "ExpirationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ClincalCompliances", "DocumentId");
        }
    }
}
