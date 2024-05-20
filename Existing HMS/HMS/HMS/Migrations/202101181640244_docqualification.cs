namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docqualification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Qualification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Qualification");
        }
    }
}
