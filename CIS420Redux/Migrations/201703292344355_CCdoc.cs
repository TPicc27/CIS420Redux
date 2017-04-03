namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CCdoc : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ClincalCompliances", "DocumentId");
            AddForeignKey("dbo.ClincalCompliances", "DocumentId", "dbo.Documents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClincalCompliances", "DocumentId", "dbo.Documents");
            DropIndex("dbo.ClincalCompliances", new[] { "DocumentId" });
        }
    }
}
