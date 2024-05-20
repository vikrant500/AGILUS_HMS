namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportcolumnsadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceReports", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.InvoiceReports", "PatientId", c => c.Long(nullable: false));
            AddColumn("dbo.InvoiceReports", "DepartmentName", c => c.String());
            AddColumn("dbo.InvoiceReports", "PaidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceReports", "PendingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceReports", "PendingAmount");
            DropColumn("dbo.InvoiceReports", "PaidAmount");
            DropColumn("dbo.InvoiceReports", "DepartmentName");
            DropColumn("dbo.InvoiceReports", "PatientId");
            DropColumn("dbo.InvoiceReports", "DateCreated");
        }
    }
}
