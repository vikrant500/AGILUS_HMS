namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoicedescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Description");
        }
    }
}
