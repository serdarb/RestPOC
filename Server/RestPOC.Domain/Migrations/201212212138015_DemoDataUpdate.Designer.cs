// <auto-generated />
namespace RestPOC.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class DemoDataUpdate : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(DemoDataUpdate));
        
        string IMigrationMetadata.Id
        {
            get { return "201212212138015_DemoDataUpdate"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
