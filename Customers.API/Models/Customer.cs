namespace Customers.API.Model
{
    public class Customer
    {
        public Customer(int id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
