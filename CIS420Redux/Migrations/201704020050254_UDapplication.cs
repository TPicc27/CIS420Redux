namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UDapplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UdApplications", "StudentNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UdApplications", "StudentNumber");
        }
    }
}
