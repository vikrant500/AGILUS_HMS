namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientReports", "Status", c => c.String());
            AddColumn("dbo.PatientReports", "DateUploaded", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientReports", "Downloaded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientReports", "Downloaded");
            DropColumn("dbo.PatientReports", "DateUploaded");
            DropColumn("dbo.PatientReports", "Status");
        }
    }
}
