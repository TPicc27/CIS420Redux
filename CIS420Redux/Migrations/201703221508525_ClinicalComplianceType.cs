namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicalComplianceType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClincalCompliances", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClincalCompliances", "Type", c => c.String());
        }
    }
}
