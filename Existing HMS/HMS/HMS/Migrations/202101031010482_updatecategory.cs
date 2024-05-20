namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfitLosses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Expence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Revenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProfitOrLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProfitLosses");
        }
    }
}
