namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientvalidationphonenumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "MarialStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "EmailID", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "CallingNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Patients", "WhatsappNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Patients", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "PinCode", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "PinCode", c => c.String());
            AlterColumn("dbo.Patients", "City", c => c.String());
            AlterColumn("dbo.Patients", "WhatsappNumber", c => c.String());
            AlterColumn("dbo.Patients", "CallingNumber", c => c.String());
            AlterColumn("dbo.Patients", "EmailID", c => c.String());
            AlterColumn("dbo.Patients", "MarialStatus", c => c.String());
            AlterColumn("dbo.Patients", "Gender", c => c.String());
        }
    }
}
