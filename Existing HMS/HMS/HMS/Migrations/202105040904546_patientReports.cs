namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientReports",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ReportName = c.String(),
                        Appointment_ID = c.Long(),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Appointments", t => t.Appointment_ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Appointment_ID)
                .Index(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientReports", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.PatientReports", "Appointment_ID", "dbo.Appointments");
            DropIndex("dbo.PatientReports", new[] { "Patient_ID" });
            DropIndex("dbo.PatientReports", new[] { "Appointment_ID" });
            DropTable("dbo.PatientReports");
        }
    }
}
