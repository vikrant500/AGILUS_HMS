namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicedepartment_addition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Department_ID", c => c.Long());
            CreateIndex("dbo.Invoices", "Department_ID");
            AddForeignKey("dbo.Invoices", "Department_ID", "dbo.Departments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Invoices", new[] { "Department_ID" });
            DropColumn("dbo.Invoices", "Department_ID");
        }
    }
}
