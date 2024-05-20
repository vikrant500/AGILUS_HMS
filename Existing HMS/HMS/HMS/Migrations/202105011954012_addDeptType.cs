namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeptType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentType_ID", c => c.Int());
            CreateIndex("dbo.Departments", "DepartmentType_ID");
            AddForeignKey("dbo.Departments", "DepartmentType_ID", "dbo.DepartmentTypes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "DepartmentType_ID", "dbo.DepartmentTypes");
            DropIndex("dbo.Departments", new[] { "DepartmentType_ID" });
            DropColumn("dbo.Departments", "DepartmentType_ID");
        }
    }
}
