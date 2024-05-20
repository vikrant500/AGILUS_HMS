namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicetransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceTransactions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModeOfPayment = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                        Invoice_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_ID)
                .Index(t => t.Invoice_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceTransactions", "Invoice_ID", "dbo.Invoices");
            DropIndex("dbo.InvoiceTransactions", new[] { "Invoice_ID" });
            DropTable("dbo.InvoiceTransactions");
        }
    }
}
