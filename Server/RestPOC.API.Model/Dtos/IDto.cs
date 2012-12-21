namespace RestPOC.API.Model.Dtos
{
    using System;

    public interface IDto {
        int Id { get; set; }
        DateTime DeletedOn { get; set; }
    }
}
