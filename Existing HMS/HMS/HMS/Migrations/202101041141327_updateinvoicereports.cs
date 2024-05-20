namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinvoicereports : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "InvoiceReport_ID", c => c.Long());
            CreateIndex("dbo.InvoiceItems", "InvoiceReport_ID");
            AddForeignKey("dbo.InvoiceItems", "InvoiceReport_ID", "dbo.InvoiceReports", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "InvoiceReport_ID", "dbo.InvoiceReports");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceReport_ID" });
            DropColumn("dbo.InvoiceItems", "InvoiceReport_ID");
        }
    }
}
