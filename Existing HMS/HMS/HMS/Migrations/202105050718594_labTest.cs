namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class labTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabTests",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TestDate = c.DateTime(nullable: false),
                        TestName = c.String(),
                        Comment = c.String(),
                        Department_ID = c.Long(),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Department_ID)
                .Index(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabTests", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.LabTests", "Department_ID", "dbo.Departments");
            DropIndex("dbo.LabTests", new[] { "Patient_ID" });
            DropIndex("dbo.LabTests", new[] { "Department_ID" });
            DropTable("dbo.LabTests");
        }
    }
}
