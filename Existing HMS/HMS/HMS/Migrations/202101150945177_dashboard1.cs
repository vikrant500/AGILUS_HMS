namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dashboard1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NumOfDoctors = c.Int(nullable: false),
                        NumOfPatients = c.Int(nullable: false),
                        AppointmentsAttended = c.Int(nullable: false),
                        AppointmentsPending = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Appointments", "Dashboard_ID", c => c.Long());
            AddColumn("dbo.Doctors", "Dashboard_ID", c => c.Long());
            AddColumn("dbo.Patients", "Dashboard_ID", c => c.Long());
            AlterColumn("dbo.Patients", "Salutation", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "MarialStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "EmailID", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "CallingNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Patients", "WhatsappNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Patients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "PinCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String(maxLength: 10));
            AlterColumn("dbo.Patients", "Occupation", c => c.String(nullable: false));
            CreateIndex("dbo.Appointments", "Dashboard_ID");
            CreateIndex("dbo.Doctors", "Dashboard_ID");
            CreateIndex("dbo.Patients", "Dashboard_ID");
            AddForeignKey("dbo.Doctors", "Dashboard_ID", "dbo.Dashboards", "ID");
            AddForeignKey("dbo.Patients", "Dashboard_ID", "dbo.Dashboards", "ID");
            AddForeignKey("dbo.Appointments", "Dashboard_ID", "dbo.Dashboards", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffSalaries", "Staff_ID", "dbo.Staffs");
            DropForeignKey("dbo.LoyalityProcedures", "Procedure_ID", "dbo.Procedures");
            DropForeignKey("dbo.LoyalityProcedures", "Loyality_ID", "dbo.Loyalities");
            DropForeignKey("dbo.Loyalities", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.LoyalityDependents", "Loyality_ID", "dbo.Loyalities");
            DropForeignKey("dbo.DoctorSalaries", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "Dashboard_ID", "dbo.Dashboards");
            DropForeignKey("dbo.Patients", "Dashboard_ID", "dbo.Dashboards");
            DropForeignKey("dbo.Doctors", "Dashboard_ID", "dbo.Dashboards");
            DropIndex("dbo.StaffSalaries", new[] { "Staff_ID" });
            DropIndex("dbo.LoyalityProcedures", new[] { "Procedure_ID" });
            DropIndex("dbo.LoyalityProcedures", new[] { "Loyality_ID" });
            DropIndex("dbo.LoyalityDependents", new[] { "Loyality_ID" });
            DropIndex("dbo.Loyalities", new[] { "Patient_ID" });
            DropIndex("dbo.DoctorSalaries", new[] { "Doctor_ID" });
            DropIndex("dbo.Patients", new[] { "Dashboard_ID" });
            DropIndex("dbo.Doctors", new[] { "Dashboard_ID" });
            DropIndex("dbo.Appointments", new[] { "Dashboard_ID" });
            AlterColumn("dbo.Patients", "Occupation", c => c.String());
            AlterColumn("dbo.Patients", "AadhaarID", c => c.String());
            AlterColumn("dbo.Patients", "PinCode", c => c.String());
            AlterColumn("dbo.Patients", "City", c => c.String());
            AlterColumn("dbo.Patients", "Address", c => c.String());
            AlterColumn("dbo.Patients", "WhatsappNumber", c => c.String());
            AlterColumn("dbo.Patients", "CallingNumber", c => c.String());
            AlterColumn("dbo.Patients", "EmailID", c => c.String());
            AlterColumn("dbo.Patients", "MarialStatus", c => c.String());
            AlterColumn("dbo.Patients", "Gender", c => c.String());
            AlterColumn("dbo.Patients", "LastName", c => c.String());
            AlterColumn("dbo.Patients", "FirstName", c => c.String());
            AlterColumn("dbo.Patients", "Salutation", c => c.String());
            DropColumn("dbo.Patients", "Dashboard_ID");
            DropColumn("dbo.Doctors", "Dashboard_ID");
            DropColumn("dbo.Appointments", "Dashboard_ID");
            DropTable("dbo.Taxes");
            DropTable("dbo.StaffSalaries");
            DropTable("dbo.LoyalityProcedures");
            DropTable("dbo.LoyalityDependents");
            DropTable("dbo.Loyalities");
            DropTable("dbo.DoctorSalaries");
            DropTable("dbo.Dashboards");
        }
    }
}
