namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "DateCreated");
        }
    }
}
