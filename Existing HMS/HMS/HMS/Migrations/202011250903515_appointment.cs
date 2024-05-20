namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(nullable: false),
                        AppointmentTime = c.Time(nullable: false, precision: 7),
                        Message = c.String(),
                        Department_ID = c.Int(),
                        Doctor_ID = c.Int(),
                        Patient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Department_ID)
                .Index(t => t.Doctor_ID)
                .Index(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropIndex("dbo.Appointments", new[] { "Department_ID" });
            DropTable("dbo.Appointments");
        }
    }
}
