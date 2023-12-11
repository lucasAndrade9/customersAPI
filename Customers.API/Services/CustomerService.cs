using Customers.API.Model;
using Customers.API.Persistence;

namespace Customers.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerPersistence _persistence;
        private static readonly object _saveCustomersLock = new();

        public CustomerService(ICustomerPersistence persistence)
        {
            _persistence = persistence;
        }

        public void Create(IEnumerable<Customer> newCustomers)
        {
            lock (_saveCustomersLock)
            {
                var customers = GetAll().ToList();

                foreach (var newCustomer in newCustomers)
                {
                    int index = 0;
                    while (index < customers.Count &&
                           string.Compare(newCustomer.LastName, customers[index].LastName, StringComparison.Ordinal) > 0)
                    {
                        index++;
                    }

                    while (index < customers.Count &&
                           string.Compare(newCustomer.LastName, customers[index].LastName, StringComparison.Ordinal) == 0 &&
                           string.Compare(newCustomer.FirstName, customers[index].FirstName, StringComparison.Ordinal) > 0)
                    {
                        index++;
                    }

                    customers.Insert(index, newCustomer);
                }

                _persistence.SaveCustomers(customers);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return _persistence.GetCustomers();
        }
    }
}
