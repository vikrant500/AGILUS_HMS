namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addappttype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "AppointType");
        }
    }
}
