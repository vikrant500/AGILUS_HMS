namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salaries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorSalaries",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidDate = c.DateTime(nullable: false),
                        Doctor_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID)
                .Index(t => t.Doctor_ID);
            
            CreateTable(
                "dbo.StaffSalaries",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidDate = c.DateTime(nullable: false),
                        Staff_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staffs", t => t.Staff_ID)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffSalaries", "Staff_ID", "dbo.Staffs");
            DropForeignKey("dbo.DoctorSalaries", "Doctor_ID", "dbo.Doctors");
            DropIndex("dbo.StaffSalaries", new[] { "Staff_ID" });
            DropIndex("dbo.DoctorSalaries", new[] { "Doctor_ID" });
            DropTable("dbo.StaffSalaries");
            DropTable("dbo.DoctorSalaries");
        }
    }
}
