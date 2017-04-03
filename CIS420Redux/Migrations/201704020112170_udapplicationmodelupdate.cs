namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udapplicationmodelupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UdApplications", "SelectProgram", c => c.String());
            AlterColumn("dbo.UdApplications", "Semester", c => c.String());
            AlterColumn("dbo.UdApplications", "Crimes", c => c.String());
            AlterColumn("dbo.UdApplications", "SchoolTrouble", c => c.String());
            AlterColumn("dbo.UdApplications", "HonorablyDischarge", c => c.String());
            AlterColumn("dbo.UdApplications", "DischargedEmployment", c => c.String());
            AlterColumn("dbo.UdApplications", "Harassment", c => c.String());
            AlterColumn("dbo.UdApplications", "DrugsOrAlcohol", c => c.String());
            AlterColumn("dbo.UdApplications", "AccurateKnowledge", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UdApplications", "AccurateKnowledge", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "DrugsOrAlcohol", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "Harassment", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "DischargedEmployment", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "HonorablyDischarge", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "SchoolTrouble", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "Crimes", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "Semester", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UdApplications", "SelectProgram", c => c.Boolean(nullable: false));
        }
    }
}
