namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class procedurestring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceReports", "ProcedureString", c => c.String());
            AddColumn("dbo.InvoiceReports", "PaymentString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceReports", "PaymentString");
            DropColumn("dbo.InvoiceReports", "ProcedureString");
        }
    }
}
