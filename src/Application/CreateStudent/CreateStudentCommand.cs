using System.Collections.Generic;

namespace Application.CreateStudent
{
    public class CreateStudentCommand
    {
        public string ItemId { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int Age { get; set; }

        public List<AddressDto> Addresses { get; set; } = new();
    }

    public class AddressDto
    {
        public string City { get; set; } = default!;

        public string Zip { get; set; } = default!;

        public string Country { get;  set; } = default!;
    }
}
