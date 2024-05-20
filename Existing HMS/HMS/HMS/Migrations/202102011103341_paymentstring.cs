namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceTransactions", "InvoiceReport_ID", c => c.Long());
            CreateIndex("dbo.InvoiceTransactions", "InvoiceReport_ID");
            AddForeignKey("dbo.InvoiceTransactions", "InvoiceReport_ID", "dbo.InvoiceReports", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceTransactions", "InvoiceReport_ID", "dbo.InvoiceReports");
            DropIndex("dbo.InvoiceTransactions", new[] { "InvoiceReport_ID" });
            DropColumn("dbo.InvoiceTransactions", "InvoiceReport_ID");
        }
    }
}
