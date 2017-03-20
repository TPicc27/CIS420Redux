namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tekk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CampusId", "dbo.Campus");
            DropIndex("dbo.Courses", new[] { "CampusId" });
            DropColumn("dbo.Courses", "CampusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CampusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CampusId");
            AddForeignKey("dbo.Courses", "CampusId", "dbo.Campus", "Id", cascadeDelete: true);
        }
    }
}
