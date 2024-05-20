namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profitlosssalary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfitLosses", "DoctorSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProfitLosses", "StaffSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfitLosses", "StaffSalary");
            DropColumn("dbo.ProfitLosses", "DoctorSalary");
        }
    }
}
