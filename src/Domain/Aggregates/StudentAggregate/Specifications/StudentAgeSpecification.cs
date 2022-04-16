namespace Domain.Aggregates.StudentAggregate.Specifications
{
    public class StudentAgeSpecification :
        BaseSpecification<Student>
    {
        public StudentAgeSpecification(int age)
            : base(s => s.Age >= age)
        {

        }
    }
}
