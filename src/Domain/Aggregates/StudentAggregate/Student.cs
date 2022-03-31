using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.StudentAggregate
{
    public class Student
    {
        [Key]
        public string ItemId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
