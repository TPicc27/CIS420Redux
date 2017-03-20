namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tepp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CatalogNumber", c => c.String());
            AlterColumn("dbo.Courses", "ProgramId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "ProgramId");
            AddForeignKey("dbo.Courses", "ProgramId", "dbo.Programs", "Id", cascadeDelete: false);
            DropColumn("dbo.Courses", "CatlogNumber");
            DropColumn("dbo.Courses", "CampusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CampusId", c => c.String());
            AddColumn("dbo.Courses", "CatlogNumber", c => c.String());
            DropForeignKey("dbo.Courses", "ProgramId", "dbo.Programs");
            DropIndex("dbo.Courses", new[] { "ProgramId" });
            AlterColumn("dbo.Courses", "ProgramId", c => c.String());
            DropColumn("dbo.Courses", "CatalogNumber");
        }
    }
}
