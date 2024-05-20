namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprofitloss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfitLosses", "From", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProfitLosses", "To", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfitLosses", "To");
            DropColumn("dbo.ProfitLosses", "From");
        }
    }
}
