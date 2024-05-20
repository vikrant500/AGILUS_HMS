namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class downloadDatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientReports", "DateDownload", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientReports", "DateDownload");
        }
    }
}
