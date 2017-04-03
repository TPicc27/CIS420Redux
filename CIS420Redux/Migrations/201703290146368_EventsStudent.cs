namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventsStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "StudentId");
            AddForeignKey("dbo.Events", "StudentId", "dbo.Students", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "StudentId", "dbo.Students");
            DropIndex("dbo.Events", new[] { "StudentId" });
            DropColumn("dbo.Events", "StudentId");
        }
    }
}
