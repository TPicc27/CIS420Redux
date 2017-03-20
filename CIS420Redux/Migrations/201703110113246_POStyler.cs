namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POStyler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course1 = c.String(),
                        Course2 = c.String(),
                        Course3 = c.String(),
                        Course4 = c.String(),
                        Course5 = c.String(),
                        Course6 = c.String(),
                        Course7 = c.String(),
                        Course8 = c.String(),
                        Course9 = c.String(),
                        Course10 = c.String(),
                        Course11 = c.String(),
                        Course12 = c.String(),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POS", "StudentId", "dbo.Students");
            DropIndex("dbo.POS", new[] { "StudentId" });
            DropTable("dbo.POS");
        }
    }
}
