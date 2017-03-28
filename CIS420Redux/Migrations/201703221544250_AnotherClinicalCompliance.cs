namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherClinicalCompliance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClincalCompliances", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClincalCompliances", "Type");
        }
    }
}
