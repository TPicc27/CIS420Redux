namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udapplicaiton : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UdApplications", "IsAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UdApplications", "IsAccepted");
        }
    }
}
