namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientfeedbackUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientFeedbacks", "Email", c => c.String());
            AddColumn("dbo.PatientFeedbacks", "PhoneNumber", c => c.String());
            AddColumn("dbo.PatientFeedbacks", "Reveiw", c => c.String());
            AddColumn("dbo.PatientFeedbacks", "ServiceOfAttendingNurses", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientFeedbacks", "ServiceOfAttendingNurses");
            DropColumn("dbo.PatientFeedbacks", "Reveiw");
            DropColumn("dbo.PatientFeedbacks", "PhoneNumber");
            DropColumn("dbo.PatientFeedbacks", "Email");
        }
    }
}
