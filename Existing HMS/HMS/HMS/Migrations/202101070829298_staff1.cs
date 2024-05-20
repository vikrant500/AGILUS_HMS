namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staff1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "DateCreated");
        }
    }
}
