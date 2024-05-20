namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientnotknowndob1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(maxLength: 12));
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: true));
        }
    }
}
