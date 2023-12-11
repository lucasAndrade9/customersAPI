using Customers.API.Model;
using System.Text.Json;

namespace Customers.API.Persistence
{
    public class CustomerPersistence : ICustomerPersistence
    {
        private const int MAX_RETRIES = 3;

        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                using var streamReader = new StreamReader("customer_data.json");
                string json = streamReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<Customer>>(json)!;
            }
            catch (FileNotFoundException)
            {
                return new List<Customer>();
            }
        }

        public void SaveCustomers(List<Customer> customers)
        {
            int retryCount = 0;

            while (retryCount < MAX_RETRIES)
            {
                try
                {
                    using var writer = new StringWriter();
                    string json = JsonSerializer.Serialize(customers);
                    File.WriteAllText("customer_data.json", json);

                    break;
                }
                catch (IOException)
                {
                    retryCount++;
                    Thread.Sleep(100);
                }
            }
        }

    }
}
