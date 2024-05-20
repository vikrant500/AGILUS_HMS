namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatient1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Occupation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Occupation", c => c.String(nullable: false));
        }
    }
}
