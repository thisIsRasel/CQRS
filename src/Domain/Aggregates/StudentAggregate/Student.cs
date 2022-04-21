﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.StudentAggregate
{
    public class Student
    {
        [Key]
        public string ItemId { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int Age { get; set; }
    }
}
