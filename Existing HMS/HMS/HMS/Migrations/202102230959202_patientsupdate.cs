namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientsupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "EmailID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "EmailID", c => c.String(nullable: false));
        }
    }
}
