namespace RestPOC.Domain {
    using System;

    public interface IEntity {
        int Id { get; set; }
        Nullable<DateTime> DeletedOn { get; set; }
    }
}