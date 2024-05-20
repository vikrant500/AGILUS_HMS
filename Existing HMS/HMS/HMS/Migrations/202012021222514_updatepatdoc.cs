namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatdoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Email", c => c.String());
            AddColumn("dbo.Doctors", "Phone", c => c.String());
            AddColumn("dbo.Doctors", "Age", c => c.String());
            AddColumn("dbo.Doctors", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Doctors", "PaymentType", c => c.String());
            AddColumn("dbo.Doctors", "Charges", c => c.Long(nullable: false));
            AddColumn("dbo.Doctors", "Speciality", c => c.String());
            AddColumn("dbo.Doctors", "Address", c => c.String());
            AddColumn("dbo.Doctors", "DateOfJoining", c => c.DateTime(nullable: false));
            DropColumn("dbo.Patients", "EmergencyContactName");
            DropColumn("dbo.Patients", "EmergencyContactRelation");
            DropColumn("dbo.Patients", "EmergencyContactNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "EmergencyContactNumber", c => c.String());
            AddColumn("dbo.Patients", "EmergencyContactRelation", c => c.String());
            AddColumn("dbo.Patients", "EmergencyContactName", c => c.String());
            DropColumn("dbo.Doctors", "DateOfJoining");
            DropColumn("dbo.Doctors", "Address");
            DropColumn("dbo.Doctors", "Speciality");
            DropColumn("dbo.Doctors", "Charges");
            DropColumn("dbo.Doctors", "PaymentType");
            DropColumn("dbo.Doctors", "DateOfBirth");
            DropColumn("dbo.Doctors", "Age");
            DropColumn("dbo.Doctors", "Phone");
            DropColumn("dbo.Doctors", "Email");
        }
    }
}
