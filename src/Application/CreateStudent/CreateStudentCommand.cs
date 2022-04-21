namespace Application.CreateStudent
{
    public class CreateStudentCommand
    {
        public string ItemId { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int Age { get; set; }
    }
}
