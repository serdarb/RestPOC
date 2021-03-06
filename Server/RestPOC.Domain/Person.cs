﻿namespace RestPOC.Domain
{
    using System;

    public class Person : IEntity
    {
        public int Id { get; set; }

        public Nullable<DateTime> DeletedOn { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int BirthYear { get; set; }

        public string MothersMaidenName { get; set; }
        public string UpdatedBy { get; set; }
    }
}
