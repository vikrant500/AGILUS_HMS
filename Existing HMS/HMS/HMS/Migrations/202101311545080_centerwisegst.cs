namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class centerwisegst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Centers", "GST", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Centers", "GST");
        }
    }
}
