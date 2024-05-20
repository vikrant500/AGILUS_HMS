namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicereports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceReports",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PatientName = c.String(),
                        Doctor = c.String(),
                        Date = c.DateTime(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvoiceReports");
        }
    }
}
