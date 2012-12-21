namespace RestPOC.Domain {
    using System;

    public interface IEntity {
        int Id { get; set; }
        DateTime DeletedOn { get; set; }
    }
}