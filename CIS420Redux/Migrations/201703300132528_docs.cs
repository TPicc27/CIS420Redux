namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "Type", c => c.String());
            AddColumn("dbo.Documents", "ComplianceStatus", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Documents", "StudentId");
            AddForeignKey("dbo.Documents", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "StudentId", "dbo.Students");
            DropIndex("dbo.Documents", new[] { "StudentId" });
            DropColumn("dbo.Documents", "ComplianceStatus");
            DropColumn("dbo.Documents", "Type");
            DropColumn("dbo.Documents", "ExpirationDate");
        }
    }
}
