namespace RestPOC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemoDataUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "MothersMaidenName", c => c.String());
            AddColumn("dbo.People", "UpdatedBy", c => c.String());
            DropColumn("dbo.People", "ApiKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "ApiKey", c => c.Guid(nullable: false));
            DropColumn("dbo.People", "UpdatedBy");
            DropColumn("dbo.People", "MothersMaidenName");
        }
    }
}
