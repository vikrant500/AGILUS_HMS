namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_biometric_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Biometrics",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(nullable: false),
                        Attendance = c.String(),
                        Overtime = c.Int(nullable: false),
                        Center = c.String(),
                        WorkingHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Biometrics");
        }
    }
}
