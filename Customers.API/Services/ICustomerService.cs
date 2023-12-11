using Customers.API.Model;

namespace Customers.API.Services
{
    public interface ICustomerService
    {
        void Create(IEnumerable<Customer> newCustomers);
        IEnumerable<Customer> GetAll();
    }
}
