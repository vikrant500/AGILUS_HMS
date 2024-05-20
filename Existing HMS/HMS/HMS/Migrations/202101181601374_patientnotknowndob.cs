namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientnotknowndob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime());
        }
    }
}
