namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PaymentMode = c.String(),
                        Sponsor = c.String(),
                        PresDoctor = c.String(),
                        RefBy = c.String(),
                        LabNo = c.String(),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Patient_ID);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Units = c.Int(nullable: false),
                        Invoice_ID = c.Long(),
                        Procedure_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_ID)
                .ForeignKey("dbo.Procedures", t => t.Procedure_ID)
                .Index(t => t.Invoice_ID)
                .Index(t => t.Procedure_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.InvoiceItems", "Procedure_ID", "dbo.Procedures");
            DropForeignKey("dbo.InvoiceItems", "Invoice_ID", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "Procedure_ID" });
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_ID" });
            DropIndex("dbo.Invoices", new[] { "Patient_ID" });
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
        }
    }
}
