namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "Grade", c => c.String());
            AddColumn("dbo.Students", "DateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "DateOfBirth");
            DropColumn("dbo.Enrollments", "Grade");
        }
    }
}
