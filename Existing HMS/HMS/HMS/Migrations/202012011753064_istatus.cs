namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class istatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Status");
        }
    }
}
