using RestSharp;
using System.Net;
using System.Text.Json;

namespace Utility
{
    public class HelperMethods
    {
        // Stores the most recently retrieved access token
        private string _accessToken = "";

        public string GetLoginDetails()
        {
            var uri = "https://qacandidatetest.ensek.io/ENSEK/login";
            var payload = "{\"username\": \"test\", \"password\": \"testing\"}";

            var options = new RestClientOptions(uri);
            var client = new RestClient(options);

            var request = new RestRequest("", Method.Post);
            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);

            if (response == null)
            {
                Console.WriteLine("Response is null.");
                return null;
            }

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response: {response.Content}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        if (doc.RootElement.TryGetProperty("access_token", out JsonElement tokenElement))
                        {
                            _accessToken = tokenElement.GetString();
                            Console.WriteLine($"Access Token: {_accessToken}");
                            return _accessToken;
                        }
                        else
                        {
                            Console.WriteLine("No 'access_token' property found in response.");
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing JSON: " + ex.Message);
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Login failed: {response.StatusCode}");
                return null;
            }
        }

        public RestResponse ResetFirst(string _accessToken)
        {            
            var uri = "https://qacandidatetest.ensek.io/ENSEK/reset";
                        
            var options = new RestClientOptions(uri);
            var client = new RestClient(options);

            // Create a new POST request to the reset endpoint
            var request = new RestRequest("", Method.Post);

            // Add required HTTP headers
            request.AddHeader("Accept", "application/json");                   
            request.AddHeader("Authorization", $"Bearer {_accessToken}");       // Add bearer token for authentication

            var response = client.Execute(request);
                        
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Post Success");
            }
            else
            {                
                Console.WriteLine($"Post request failed: {response.StatusCode}");
                Console.WriteLine(response.Content);
            }
                        
            return response;
        }
    }
}


