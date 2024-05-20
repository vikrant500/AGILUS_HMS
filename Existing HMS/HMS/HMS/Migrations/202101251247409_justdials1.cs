namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justdials1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JustDials",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Message = c.String(),
                        Patient_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Patient_ID);
            
        }
        
        public override void Down()
        {
          
        }
    }
}
