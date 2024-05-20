namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deptinproc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Procedures", "Department_ID", c => c.Long());
            CreateIndex("dbo.Procedures", "Department_ID");
            AddForeignKey("dbo.Procedures", "Department_ID", "dbo.Departments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Procedures", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Procedures", new[] { "Department_ID" });
            DropColumn("dbo.Procedures", "Department_ID");
        }
    }
}
