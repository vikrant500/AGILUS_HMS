namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorschedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorSchedules",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DaysAvailable = c.String(),
                        AvailableTime = c.String(),
                        Status = c.String(),
                        Doctor_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID)
                .Index(t => t.Doctor_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorSchedules", "Doctor_ID", "dbo.Doctors");
            DropIndex("dbo.DoctorSchedules", new[] { "Doctor_ID" });
            DropTable("dbo.DoctorSchedules");
        }
    }
}
