namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClincalComplianceStudentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClincalCompliances", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClincalCompliances", "StudentId");
            AddForeignKey("dbo.ClincalCompliances", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            DropColumn("dbo.ClincalCompliances", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClincalCompliances", "Status", c => c.String());
            DropForeignKey("dbo.ClincalCompliances", "StudentId", "dbo.Students");
            DropIndex("dbo.ClincalCompliances", new[] { "StudentId" });
            DropColumn("dbo.ClincalCompliances", "StudentId");
        }
    }
}
