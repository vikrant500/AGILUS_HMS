namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceDoctorAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Doctor_ID", c => c.Long());
            CreateIndex("dbo.Invoices", "Doctor_ID");
            AddForeignKey("dbo.Invoices", "Doctor_ID", "dbo.Doctors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Doctor_ID", "dbo.Doctors");
            DropIndex("dbo.Invoices", new[] { "Doctor_ID" });
            DropColumn("dbo.Invoices", "Doctor_ID");
        }
    }
}
