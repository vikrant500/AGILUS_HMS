namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loyalitydependents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoyalityDependents", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoyalityDependents", "Name");
        }
    }
}
