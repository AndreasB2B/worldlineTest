using Microsoft.AspNetCore.Mvc;

namespace WorldLineTest.Controllers
{
    public class WorldlineTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public void test()
        {
            string accessToken = "Qf1pCoxVLSO1fMLV70Rz";
            string merchantNumber = "T349849701";
            string secretToken = "DOhDxQWiDkWOy9oCWWGbGlSbVBiGnc6QSn5bXNRL";
            string unencodedApiKey = string.Concat(accessToken, "@", merchantNumber, ":", secretToken);
            byte[] unencodedApiKeyAsBytes = System.Text.Encoding.UTF8.GetBytes(unencodedApiKey);
            string apiKey = "Basic " + System.Convert.ToBase64String(unencodedApiKeyAsBytes);
            string endpoint = "https://api.v1.checkout.bambora.com/sessions";
            dynamic checkoutRequest = new
            {
                order = new
                {
                    id = "ABC123",
                    amount = "195",
                    currency = "DKK"
                },
                url = new
                {
                    accept = "https://example.org/accept",
                    cancel = "https://example.org/cancel",
                    callbacks = new List<dynamic> { new { url = "https://example.org/callback" } }
                }
            };
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(System.Net.HttpRequestHeader.Authorization, apiKey);
            client.Headers.Add(System.Net.HttpRequestHeader.Accept, "application/json");

            var checkoutRequestJson = System.Web.Helpers.Json.Encode(checkoutRequest);
            var checkoutResponseJson = client.UploadString(endpoint, "POST", checkoutRequestJson);
            dynamic checkoutResponse = System.Web.Helpers.Json.Decode(checkoutResponseJson);
        }
    }
}
