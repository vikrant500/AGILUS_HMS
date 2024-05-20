namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qualificationfordocsandstaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "Qualification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "Qualification");
        }
    }
}
