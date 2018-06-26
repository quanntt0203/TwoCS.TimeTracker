namespace TwoCS.TimeTracker.XUnitTest
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Helpers;
    using TwoCS.TimeTracker.Core.Results;
    using TwoCS.TimeTracker.Dto;
    using Xunit;

    public class OAuthController_Test : XUnitBase
    {
        public OAuthController_Test()
        {
        }

        [Fact]
        public async Task PassingToken()
        {
            var dto = new LoginUserDto
            {
                Email = "admin@2cs.com",
                Password = "AbC!123open"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/oauth/connect-token")
            {
                Content = dto.ObjToHttpContent()
            };

            var response = await XClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var content = await response.Content.ReadAsStringAsync();

            var payload = content.JsonToObj<ApiResultOk>();

            Assert.Equal("Ok", payload.Message);
        }

        [Fact]
        public async Task FailingToken()
        {
            var dto = new LoginUserDto
            {
                Email = "admin@2cs.com",
                Password = "AbC!123openFAILED"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/oauth/connect-token")
            {
                Content = dto.ObjToHttpContent()
            };

            var response = await XClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var content = await response.Content.ReadAsStringAsync();

            var payload = content.JsonToObj<ApiResultOk>();

            Assert.NotEqual("Ok", payload.Message);
        }
    }
}
