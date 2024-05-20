namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventorychanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Option", c => c.String());
            AddColumn("dbo.Inventories", "Staff_ID", c => c.Long());
            CreateIndex("dbo.Inventories", "Staff_ID");
            AddForeignKey("dbo.Inventories", "Staff_ID", "dbo.Staffs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "Staff_ID", "dbo.Staffs");
            DropIndex("dbo.Inventories", new[] { "Staff_ID" });
            DropColumn("dbo.Inventories", "Staff_ID");
            DropColumn("dbo.Inventories", "Option");
        }
    }
}
