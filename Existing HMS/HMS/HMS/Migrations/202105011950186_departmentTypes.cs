namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DepartmentTypes");
        }
    }
}
