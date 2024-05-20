namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientvalidationadharid1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String());
        }
    }
}
