namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CatalogNumber", c => c.String());
            DropColumn("dbo.Courses", "CatlogNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CatlogNumber", c => c.String());
            DropColumn("dbo.Courses", "CatalogNumber");
        }
    }
}
