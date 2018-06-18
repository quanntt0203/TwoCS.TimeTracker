

namespace TwoCS.TimeTracker.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Helpers;
    using TwoCS.TimeTracker.Dto;

    public class ProxyService : IProxyService
    {
        private readonly Uri _proxyOAuthUri;

        public ProxyService()
        {
            _proxyOAuthUri =  new Uri("http://localhost:8000");
        }

        public async Task<JObject> AddAccountToRoletAsync(string userName, string roleName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _proxyOAuthUri;

                var dto = new { UserName = userName, Role = roleName };

                var request = new HttpRequestMessage(HttpMethod.Post, "/api/account/roles")
                {
                    Content = dto.ObjToHttpContent()
                };

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {

                    throw new BadRequestException(response.StatusCode.ToString(), new string[] { response.ReasonPhrase });
                }

                var content = await response.Content.ReadAsStringAsync();

                var payload = JObject.Parse(content);

                return payload;
            }
        }

        public async  Task<JObject> CreateAccountAsync(RegisterUserDto dto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _proxyOAuthUri;

                var request = new HttpRequestMessage(HttpMethod.Post, "/api/account/register")
                {
                    Content = dto.ObjToHttpContent()
                };

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {

                    throw new BadRequestException(response.StatusCode.ToString(), new string[] { response.ReasonPhrase });
                }

                var content = await response.Content.ReadAsStringAsync();

                var payload = JObject.Parse(content);

                return payload;
            }
        }

        public async Task<JObject> GetTokenAsync(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _proxyOAuthUri;

                var request = new HttpRequestMessage(HttpMethod.Post, "/connect/token")
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "password",
                        ["username"] = email,
                        ["password"] = password
                    })
                };

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new BadRequestException("The username/password couple is invalid.");
                }

                var content = await response.Content.ReadAsStringAsync();

                return JObject.Parse(content);
            }
        }
    }
}
