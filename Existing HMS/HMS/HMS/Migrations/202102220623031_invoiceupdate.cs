namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceDisplayID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "InvoiceDisplayID");
        }
    }
}
