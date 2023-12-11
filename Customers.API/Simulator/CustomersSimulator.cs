using Customers.API.Model;
using Customers.API.Models;
using System.Text.Json;

namespace Customers.API.Simulator
{
    public class CustomersSimulator : BackgroundService
    {
        private const string BASE_URI = "https://localhost:7244/";
        private static int _lastCustomerId = 0;
        private readonly HttpClient _client;

        public CustomersSimulator(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                var customersInputModel = GenerateCustomersInputModel();
                var json = JsonSerializer.Serialize(customersInputModel);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                tasks.Add(Task.Run(async () =>
                {
                    var postResponse = await _client.PostAsync($"{BASE_URI}api/customers", content);
                }, stoppingToken));
            }

            await Task.WhenAll(tasks);
        }

        private static CustomerInputModel GenerateCustomersInputModel()
        {
            var random = new Random();
            var firstNames = new List<string>
            {
                "Leia", "Sadie", "Jose", "Sara", 
                "Frank", "Dewey", "Tomas", "Joel", 
                "Lukas", "Carlos"
            };
            var lastNames = new List<string>
            {
                "Liberty", "Ray", "Harrison", "Ronan", 
                "Drew", "Powell", "Larsen", "Chan", 
                "Anderson", "Lane"
            };

            var newCustomers = new List<Customer>
            {
                new Customer(++_lastCustomerId, GetRandomElement(firstNames), GetRandomElement(lastNames), random.Next(10, 91)),
                new Customer(++_lastCustomerId, GetRandomElement(firstNames), GetRandomElement(lastNames), random.Next(10, 91)),
            };

            return new CustomerInputModel { Customers = newCustomers };
        }

        private static T GetRandomElement<T>(List<T> values)
        {
            var index = new Random().Next(values.Count);
            return values[index];
        }
    }
}
