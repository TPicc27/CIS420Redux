namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ccdoc2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClincalCompliances", "DocumentId", "dbo.Documents");
            DropIndex("dbo.ClincalCompliances", new[] { "DocumentId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ClincalCompliances", "DocumentId");
            AddForeignKey("dbo.ClincalCompliances", "DocumentId", "dbo.Documents", "Id");
        }
    }
}
