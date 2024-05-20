namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetdobpatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.Doctors", "Qualification");
            DropColumn("dbo.Staffs", "Qualification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "Qualification", c => c.String());
            AddColumn("dbo.Doctors", "Qualification", c => c.String());
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime());
        }
    }
}
