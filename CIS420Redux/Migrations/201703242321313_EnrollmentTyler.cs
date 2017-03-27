namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnrollmentTyler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "Semester", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "Semester");
        }
    }
}
