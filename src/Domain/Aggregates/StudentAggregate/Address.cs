using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.StudentAggregate
{
    public class Address
    {
        [Key]
        public string ItemId { get; set; }

        public string City { get; }

        public string Zip { get; }
         
        public string Country { get; }
    }
}
