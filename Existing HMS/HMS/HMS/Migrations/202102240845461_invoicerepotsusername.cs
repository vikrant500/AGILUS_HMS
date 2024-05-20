namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicerepotsusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceReports", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceReports", "CreatedBy");
        }
    }
}
