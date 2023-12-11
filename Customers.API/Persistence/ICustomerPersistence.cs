using Customers.API.Model;

namespace Customers.API.Persistence
{
    public interface ICustomerPersistence
    {
        IEnumerable<Customer> GetCustomers();
        void SaveCustomers(List<Customer> customers);
    }
}
