namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientfeedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientFeedbacks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PatientName = c.String(),
                        PatientAddress = c.String(),
                        ConsultedDoctor = c.String(),
                        Environment = c.Int(nullable: false),
                        WardFacilities = c.Int(nullable: false),
                        ServiceOfAttendingDoctors = c.Int(nullable: false),
                        Billing = c.Int(nullable: false),
                        EnquiryService = c.Int(nullable: false),
                        AdmissionProcess = c.Int(nullable: false),
                        DischargeProcess = c.Int(nullable: false),
                        SujjestionsToImprove = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientFeedbacks");
        }
    }
}
