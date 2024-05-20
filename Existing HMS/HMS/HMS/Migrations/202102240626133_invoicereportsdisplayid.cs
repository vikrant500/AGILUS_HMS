namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicereportsdisplayid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceReports", "InvoiceDisplayId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceReports", "InvoiceDisplayId");
        }
    }
}
