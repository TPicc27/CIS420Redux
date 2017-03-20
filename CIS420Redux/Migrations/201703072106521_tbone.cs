namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "ProgramId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "CampusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "ProgramId");
            CreateIndex("dbo.Courses", "CampusId");
            AddForeignKey("dbo.Courses", "CampusId", "dbo.Campus", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Courses", "ProgramId", "dbo.Programs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Courses", "CampusId", "dbo.Campus");
            DropIndex("dbo.Courses", new[] { "CampusId" });
            DropIndex("dbo.Courses", new[] { "ProgramId" });
            AlterColumn("dbo.Courses", "CampusId", c => c.String());
            AlterColumn("dbo.Courses", "ProgramId", c => c.String());
        }
    }
}
