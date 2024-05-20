namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFeedbackForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientFeedbacks", "Receptionist_Behaviour", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "WaitingTime", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "Hygiene", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "ExperienceWithDoc", c => c.Int(nullable: false));
            DropColumn("dbo.PatientFeedbacks", "Reveiw");
            DropColumn("dbo.PatientFeedbacks", "Environment");
            DropColumn("dbo.PatientFeedbacks", "WardFacilities");
            DropColumn("dbo.PatientFeedbacks", "ServiceOfAttendingDoctors");
            DropColumn("dbo.PatientFeedbacks", "ServiceOfAttendingNurses");
            DropColumn("dbo.PatientFeedbacks", "EnquiryService");
            DropColumn("dbo.PatientFeedbacks", "AdmissionProcess");
            DropColumn("dbo.PatientFeedbacks", "DischargeProcess");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientFeedbacks", "DischargeProcess", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "AdmissionProcess", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "EnquiryService", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "ServiceOfAttendingNurses", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "ServiceOfAttendingDoctors", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "WardFacilities", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "Environment", c => c.Int(nullable: false));
            AddColumn("dbo.PatientFeedbacks", "Reveiw", c => c.String());
            DropColumn("dbo.PatientFeedbacks", "ExperienceWithDoc");
            DropColumn("dbo.PatientFeedbacks", "Hygiene");
            DropColumn("dbo.PatientFeedbacks", "WaitingTime");
            DropColumn("dbo.PatientFeedbacks", "Receptionist_Behaviour");
        }
    }
}
