namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String());
        }
    }
}
