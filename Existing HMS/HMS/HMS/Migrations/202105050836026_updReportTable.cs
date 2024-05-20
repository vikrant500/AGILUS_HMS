namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updReportTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientReports", "Appointment_ID", "dbo.Appointments");
            DropIndex("dbo.PatientReports", new[] { "Appointment_ID" });
            AddColumn("dbo.PatientReports", "LabTest_ID", c => c.Long());
            CreateIndex("dbo.PatientReports", "LabTest_ID");
            AddForeignKey("dbo.PatientReports", "LabTest_ID", "dbo.LabTests", "ID");
            DropColumn("dbo.PatientReports", "Appointment_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientReports", "Appointment_ID", c => c.Long());
            DropForeignKey("dbo.PatientReports", "LabTest_ID", "dbo.LabTests");
            DropIndex("dbo.PatientReports", new[] { "LabTest_ID" });
            DropColumn("dbo.PatientReports", "LabTest_ID");
            CreateIndex("dbo.PatientReports", "Appointment_ID");
            AddForeignKey("dbo.PatientReports", "Appointment_ID", "dbo.Appointments", "ID");
        }
    }
}
