namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorschedule1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorSchedules", "From", c => c.String());
            AddColumn("dbo.DoctorSchedules", "To", c => c.String());
            DropColumn("dbo.DoctorSchedules", "AvailableTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorSchedules", "AvailableTime", c => c.String());
            DropColumn("dbo.DoctorSchedules", "To");
            DropColumn("dbo.DoctorSchedules", "From");
        }
    }
}
