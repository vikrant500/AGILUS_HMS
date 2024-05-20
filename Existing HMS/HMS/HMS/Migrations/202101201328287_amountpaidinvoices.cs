namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountpaidinvoices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "AmountPaid");
        }
    }
}
