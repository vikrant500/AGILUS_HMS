namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "LastName", c => c.String(nullable: false));
        }
    }
}
