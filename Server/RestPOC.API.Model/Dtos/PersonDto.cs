namespace RestPOC.API.Model.Dtos {
    using System;

    public class PersonDto : IDto
    {
        public int Id { get; set; }

        public DateTime DeletedOn { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}