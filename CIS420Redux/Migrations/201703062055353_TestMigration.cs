namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ProgramId", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "CPR_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "HIPPA_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Bloodbourne_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Liability_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Immunization_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Drug_Screen_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "CNA_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Is_Compliant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "AdvisorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "CampusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "CampusId");
            CreateIndex("dbo.Students", "ProgramId");
            AddForeignKey("dbo.Students", "CampusId", "dbo.Campus", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Students", "ProgramId", "dbo.Programs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Students", "CampusId", "dbo.Campus");
            DropIndex("dbo.Students", new[] { "ProgramId" });
            DropIndex("dbo.Students", new[] { "CampusId" });
            AlterColumn("dbo.Students", "CampusId", c => c.String());
            DropColumn("dbo.Students", "AdvisorID");
            DropColumn("dbo.Students", "Is_Compliant");
            DropColumn("dbo.Students", "CNA_Compliant");
            DropColumn("dbo.Students", "Drug_Screen_Compliant");
            DropColumn("dbo.Students", "Immunization_Compliant");
            DropColumn("dbo.Students", "Liability_Compliant");
            DropColumn("dbo.Students", "Bloodbourne_Compliant");
            DropColumn("dbo.Students", "HIPPA_Compliant");
            DropColumn("dbo.Students", "CPR_Compliant");
            DropColumn("dbo.Students", "ProgramId");
        }
    }
}
