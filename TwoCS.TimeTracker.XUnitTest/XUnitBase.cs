namespace TwoCS.TimeTracker.XUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class XUnitBase
    {
        private readonly string userName = "admin@2cs.com";
        private readonly string password = "AbC!123open";

        protected HttpClient XClient;

        public XUnitBase()
        {
            XClient = new HttpClient();
            XClient.BaseAddress = new Uri("http://localhost:8000");
        }

        public async Task<string> GetToken()
        {

            return await GetToken(userName, password);
        }

        public async Task<string> GetToken(string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = email,
                ["password"] = password
            });

            var response = await XClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var payload = JObject.Parse(content);
            if (payload["error"] != null)
            {
                throw new InvalidOperationException("An error occurred while retrieving an access token.");
            }

            return (string)payload["access_token"];
        }
    }
}
