namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UdApplications", "Status", c => c.String());
            DropColumn("dbo.UdApplications", "IsAccepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UdApplications", "IsAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.UdApplications", "Status");
        }
    }
}
