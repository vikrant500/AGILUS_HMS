namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicesupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "LastModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "LastModifiedDate");
        }
    }
}
