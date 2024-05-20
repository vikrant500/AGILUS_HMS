namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "AppointmentTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
