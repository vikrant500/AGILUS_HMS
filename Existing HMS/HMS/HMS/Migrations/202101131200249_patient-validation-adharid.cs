namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientvalidationadharid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(nullable: false));
        }
    }
}
