namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicediscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoices", "DiscountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "DiscountAmount");
            DropColumn("dbo.Invoices", "DiscountPercentage");
        }
    }
}
