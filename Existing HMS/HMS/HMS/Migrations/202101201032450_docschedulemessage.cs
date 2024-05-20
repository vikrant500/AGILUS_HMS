namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docschedulemessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorSchedules", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoctorSchedules", "Message");
        }
    }
}
