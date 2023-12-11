using Customers.API.Model;

namespace Customers.API.Models
{
    public class CustomerInputModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
