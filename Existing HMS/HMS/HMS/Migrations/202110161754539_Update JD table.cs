namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJDtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JustDials", "Department", c => c.String());
            AddColumn("dbo.JustDials", "Procedure", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JustDials", "Procedure");
            DropColumn("dbo.JustDials", "Department");
        }
    }
}
