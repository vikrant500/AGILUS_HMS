namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatejustdial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JustDials", "Patient_ID", "dbo.Patients");
            DropIndex("dbo.JustDials", new[] { "Patient_ID" });
            AddColumn("dbo.JustDials", "PatientName", c => c.String());
            DropColumn("dbo.JustDials", "Patient_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JustDials", "Patient_ID", c => c.Long());
            DropColumn("dbo.JustDials", "PatientName");
            CreateIndex("dbo.JustDials", "Patient_ID");
            AddForeignKey("dbo.JustDials", "Patient_ID", "dbo.Patients", "ID");
        }
    }
}
