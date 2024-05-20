namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class procedure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "Department_ID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropIndex("dbo.Doctors", new[] { "Department_ID" });
            DropPrimaryKey("dbo.Appointments");
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.Doctors");
            DropPrimaryKey("dbo.Patients");
            DropPrimaryKey("dbo.Centers");
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Appointments", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Appointments", "Department_ID", c => c.Long());
            AlterColumn("dbo.Appointments", "Doctor_ID", c => c.Long());
            AlterColumn("dbo.Appointments", "Patient_ID", c => c.Long());
            AlterColumn("dbo.Departments", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Doctors", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Doctors", "Department_ID", c => c.Long());
            AlterColumn("dbo.Patients", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Centers", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Appointments", "ID");
            AddPrimaryKey("dbo.Departments", "ID");
            AddPrimaryKey("dbo.Doctors", "ID");
            AddPrimaryKey("dbo.Patients", "ID");
            AddPrimaryKey("dbo.Centers", "ID");
            CreateIndex("dbo.Appointments", "Department_ID");
            CreateIndex("dbo.Appointments", "Doctor_ID");
            CreateIndex("dbo.Appointments", "Patient_ID");
            CreateIndex("dbo.Doctors", "Department_ID");
            AddForeignKey("dbo.Appointments", "Department_ID", "dbo.Departments", "ID");
            AddForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments", "ID");
            AddForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors", "ID");
            AddForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Appointments", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Doctors", new[] { "Department_ID" });
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropIndex("dbo.Appointments", new[] { "Department_ID" });
            DropPrimaryKey("dbo.Centers");
            DropPrimaryKey("dbo.Patients");
            DropPrimaryKey("dbo.Doctors");
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.Appointments");
            AlterColumn("dbo.Centers", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Patients", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Doctors", "Department_ID", c => c.Int());
            AlterColumn("dbo.Doctors", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Departments", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Appointments", "Patient_ID", c => c.Int());
            AlterColumn("dbo.Appointments", "Doctor_ID", c => c.Int());
            AlterColumn("dbo.Appointments", "Department_ID", c => c.Int());
            AlterColumn("dbo.Appointments", "ID", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Procedures");
            AddPrimaryKey("dbo.Centers", "ID");
            AddPrimaryKey("dbo.Patients", "ID");
            AddPrimaryKey("dbo.Doctors", "ID");
            AddPrimaryKey("dbo.Departments", "ID");
            AddPrimaryKey("dbo.Appointments", "ID");
            CreateIndex("dbo.Doctors", "Department_ID");
            CreateIndex("dbo.Appointments", "Patient_ID");
            CreateIndex("dbo.Appointments", "Doctor_ID");
            CreateIndex("dbo.Appointments", "Department_ID");
            AddForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients", "ID");
            AddForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors", "ID");
            AddForeignKey("dbo.Doctors", "Department_ID", "dbo.Departments", "ID");
            AddForeignKey("dbo.Appointments", "Department_ID", "dbo.Departments", "ID");
        }
    }
}
