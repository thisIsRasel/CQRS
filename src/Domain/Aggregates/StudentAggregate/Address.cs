using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.StudentAggregate
{
    public class Address
    {
        [Key]
        public string ItemId { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Zip { get; set; } = default!;

        public string Country { get; set; } = default!;
    }
}
