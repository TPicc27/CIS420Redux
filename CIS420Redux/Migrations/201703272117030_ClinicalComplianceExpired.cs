namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicalComplianceExpired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClincalCompliances", "IsExpired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClincalCompliances", "IsExpired");
        }
    }
}
