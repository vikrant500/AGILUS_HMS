namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetdocstaffqualification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Qualification", c => c.String());
            AddColumn("dbo.Staffs", "Qualification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "Qualification");
            DropColumn("dbo.Doctors", "Qualification");
        }
    }
}
