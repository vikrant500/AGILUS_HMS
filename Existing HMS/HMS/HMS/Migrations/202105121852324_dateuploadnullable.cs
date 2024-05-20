namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateuploadnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientReports", "DateUploaded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientReports", "DateUploaded", c => c.DateTime(nullable: false));
        }
    }
}
