namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loyality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loyalities",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Patient_ID);
            
            CreateTable(
                "dbo.LoyalityDependents",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Relation = c.String(),
                        Gender = c.String(),
                        Age = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Loyality_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Loyalities", t => t.Loyality_ID)
                .Index(t => t.Loyality_ID);
            
            CreateTable(
                "dbo.LoyalityProcedures",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Loyality_ID = c.Long(),
                        Procedure_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Loyalities", t => t.Loyality_ID)
                .ForeignKey("dbo.Procedures", t => t.Procedure_ID)
                .Index(t => t.Loyality_ID)
                .Index(t => t.Procedure_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoyalityProcedures", "Procedure_ID", "dbo.Procedures");
            DropForeignKey("dbo.LoyalityProcedures", "Loyality_ID", "dbo.Loyalities");
            DropForeignKey("dbo.Loyalities", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.LoyalityDependents", "Loyality_ID", "dbo.Loyalities");
            DropIndex("dbo.LoyalityProcedures", new[] { "Procedure_ID" });
            DropIndex("dbo.LoyalityProcedures", new[] { "Loyality_ID" });
            DropIndex("dbo.LoyalityDependents", new[] { "Loyality_ID" });
            DropIndex("dbo.Loyalities", new[] { "Patient_ID" });
            DropTable("dbo.LoyalityProcedures");
            DropTable("dbo.LoyalityDependents");
            DropTable("dbo.Loyalities");
        }
    }
}
