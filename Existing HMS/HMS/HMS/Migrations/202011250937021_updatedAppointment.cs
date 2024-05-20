namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Status", c => c.String());
            AddColumn("dbo.Appointments", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "LastModifiedDatetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "LastModifiedDatetime");
            DropColumn("dbo.Appointments", "CreatedDateTime");
            DropColumn("dbo.Appointments", "Status");
        }
    }
}
