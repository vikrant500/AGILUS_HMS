namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientvalidationoccupatio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Salutation", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Occupation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Occupation", c => c.String());
            AlterColumn("dbo.Patients", "Address", c => c.String());
            AlterColumn("dbo.Patients", "Salutation", c => c.String());
        }
    }
}
