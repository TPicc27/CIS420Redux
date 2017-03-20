namespace CIS420Redux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tyty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Description", c => c.String());
            AddColumn("dbo.Todoes", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Todoes", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todoes", "EndDate");
            DropColumn("dbo.Todoes", "StartDate");
            DropColumn("dbo.Todoes", "Description");
        }
    }
}
