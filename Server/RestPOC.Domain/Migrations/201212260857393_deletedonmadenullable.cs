namespace RestPOC.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedonmadenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "DeletedOn", c => c.DateTime(nullable: false));
        }
    }
}
