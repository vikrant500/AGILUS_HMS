namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Age = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        Charges = c.Long(nullable: false),
                        Speciality = c.String(),
                        Address = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        Department_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .Index(t => t.Department_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Staffs", new[] { "Department_ID" });
            DropTable("dbo.Staffs");
        }
    }
}
