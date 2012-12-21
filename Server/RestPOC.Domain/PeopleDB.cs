namespace RestPOC.Domain {
    using System.Data.Entity;

    public class PeopleDB : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}