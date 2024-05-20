namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicecreateduser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "CreatedBy");
        }
    }
}
