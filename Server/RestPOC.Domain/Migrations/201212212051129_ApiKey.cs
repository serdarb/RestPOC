namespace RestPOC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "ApiKey", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "ApiKey");
        }
    }
}
