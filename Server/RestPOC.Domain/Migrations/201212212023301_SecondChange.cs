namespace RestPOC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DeletedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.People", "Telephone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Telephone");
            DropColumn("dbo.People", "DeletedOn");
        }
    }
}
