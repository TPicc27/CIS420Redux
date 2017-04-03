namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Application : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UdApplications", "CampusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UdApplications", "CampusId", c => c.String());
        }
    }
}
