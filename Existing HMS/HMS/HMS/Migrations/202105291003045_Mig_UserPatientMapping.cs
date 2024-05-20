namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_UserPatientMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPatientMappings",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.String(),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPatientMappings", "Patient_ID", "dbo.Patients");
            DropIndex("dbo.UserPatientMappings", new[] { "Patient_ID" });
            DropTable("dbo.UserPatientMappings");
        }
    }
}
