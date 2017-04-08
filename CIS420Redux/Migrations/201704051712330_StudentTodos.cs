namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTodos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "StudentId", "dbo.Students");
            DropIndex("dbo.Events", new[] { "StudentId" });
            DropColumn("dbo.Events", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "StudentId");
            AddForeignKey("dbo.Events", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
