using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
namespace BL
{
    public class TranslateBL
    {
        private static readonly string subscriptionKey = "91d93a27-0dcf-4c58-a41f-c2efb380fbb9";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "East US";

        public static async Task<string> Translate(string text)
        //public static void translateByMicrosoft(string sentenceToTranslate)
        {
            // Input and output languages are defined as parameters.
            string route = "/translate?api-version=3.0&from=he&to=en";
            // string textToTranslate = "Hello, world!";//args[0] ???
            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                //  HttpResponseMessage response = client.SendAsync(request).ConfigureAwait(false);

                // Read response as a string.
                return await response.Content.ReadAsStringAsync();
                //Console.WriteLine(result);

            }
        }
    }
}