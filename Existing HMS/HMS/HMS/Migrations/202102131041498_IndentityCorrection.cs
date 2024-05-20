namespace HMS.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndentityCorrection : DbMigration
    {
        public override void Up()
        {
            
                Sql("DBCC CHECKIDENT ('Invoices', RESEED, 10000);");
                Sql("DBCC CHECKIDENT ('Patients', RESEED, 10000);");
            
        }
        
        public override void Down()
        {
        }
    }
}
