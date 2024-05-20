namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientsage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Age", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Age", c => c.Int(nullable: false));
        }
    }
}
