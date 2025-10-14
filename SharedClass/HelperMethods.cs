using Newtonsoft.Json.Linq;
using RestSharp;
using Utility.Models;

namespace Utility
{
    public class HelperMethods
    {
        private const string baseUrl = "https://qacandidatetest.ensek.io";
        private static string token = "";

        public async static Task Login()
        {
            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest("ENSEK/login", Method.Post);  // defaults to GET

            var loginObj = new
            {
                username = "test",
                password = "testing"
            };

            // Add two form parameters (key/value)
            request.AddJsonBody(loginObj);
            request.AddHeader("Accept", "application/json");

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Parse into a JObject
                var obj = JObject.Parse(response.Content);
                token = (string)obj["access_token"];
            }
            else
            {
                // Handle failure
                Console.WriteLine($"Error: {response.StatusCode} {response.ErrorMessage}");
                throw new Exception("Login failed");
            }
        }

        public async static Task Reset()
        {

            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest("ENSEK/reset", Method.Post);  // defaults to GET

            // Add the Bearer token in Authorization header
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Handle success
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                // Handle failure
                Console.WriteLine($"Error: {response.StatusCode} {response.ErrorMessage}");
            }

        }

        public async static Task<Energy> GetEnergyData()
        {
            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest("ENSEK/energy", Method.Get);  // defaults to GET

            // Add the Bearer token in Authorization header
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await client.ExecuteAsync<Energy>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                // Handle failure
                throw new Exception($"Error: {response.StatusCode} {response.ErrorMessage}");
            }

        }

        public async static Task<Order[]> GetOrders()
        {
            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest("ENSEK/orders", Method.Get);  // defaults to GET

            // Add the Bearer token in Authorization header
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await client.ExecuteAsync<Order[]>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                // Handle failure
                throw new Exception($"Error: {response.StatusCode} {response.ErrorMessage}");
            }

        }

        public async static Task BuyQuantity(int id, int quantity)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"ENSEK/buy/{id}/{quantity}", Method.Put);  // defaults to GET

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                // Handle failure
                Console.WriteLine($"Error: {response.StatusCode} {response.ErrorMessage}");
                throw new Exception("Login failed");
            }
        }

        public async static Task PlaceOrder(int id, int quantity, int energyId)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"ENSEK/buy/{id}/{quantity}", Method.Put);  // defaults to GET

            var order = new
            {
                Id = id,
                Quantity = quantity,
                EnergyId = energyId
            };

            // Add two form parameters (key/value)
            request.AddJsonBody(order);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                // Handle failure
                Console.WriteLine($"Error: {response.StatusCode} {response.ErrorMessage}");
                throw new Exception("Login failed");
            }
        }

        public async static Task<Order> GetOrder(string orderid)
        {
            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest($"ENSEK/orders/{orderid}", Method.Get);  // defaults to GET

            // Add the Bearer token in Authorization header
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await client.ExecuteAsync<Order>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                // Handle failure
                throw new Exception($"Error: {response.StatusCode} {response.ErrorMessage}");
            }
        }

        public async static Task DeleteOrder(string orderid)
        {
            // Create client (base URL)
            var client = new RestClient(baseUrl);

            var request = new RestRequest($"ENSEK/orders/{orderid}", Method.Delete);  // defaults to GET

            // Add the Bearer token in Authorization header
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                //return response.Data;
            }
            else
            {
                // Handle failure
                throw new Exception($"Error: {response.StatusCode} {response.ErrorMessage}");
            }
        }
    }
}
