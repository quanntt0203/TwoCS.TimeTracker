namespace TwoCS.TimeTracker.XUnitTest
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Helpers;
    using TwoCS.TimeTracker.Core.Results;
    using Xunit;

    public class UserController_Test : XUnitBase
    {
        public UserController_Test()
        {
        }

        [Fact]
        public async Task PassingSeach()
        {
            var token = await GetToken();

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/user");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await XClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var content = await response.Content.ReadAsStringAsync();

            var payload = content.JsonToObj<ApiResultOk>();

            Assert.Equal("Ok", payload.Message);

        }
    }
}
