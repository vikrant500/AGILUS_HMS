namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventory_modeofpayment_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "ModeOfPayment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "ModeOfPayment");
        }
    }
}
