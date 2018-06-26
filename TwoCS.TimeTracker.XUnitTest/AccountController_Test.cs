namespace TwoCS.TimeTracker.XUnitTest
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Helpers;
    using TwoCS.TimeTracker.Core.Results;
    using TwoCS.TimeTracker.Dto;
    using Xunit;

    public class AccountController_Test : XUnitBase
    {
        public AccountController_Test()
        {
        }

        [Fact]
        public async Task PassingRegister()
        {
            var dto = new RegisterUserDto
            {
                UserName = "xunittest",
                Email = "xunittest@2cs.com",
                Password = "AbC!123open",
                Role = "User"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/account/register-user")
            {
                Content = dto.ObjToHttpContent()
            };

            var response = await XClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var content = await response.Content.ReadAsStringAsync();

            var payload = content.JsonToObj<ApiResultOk>();

            Assert.Equal("Ok", payload.Message);
        }

        [Fact]
        public async Task FailingRegister()
        {
            var dto = new RegisterUserDto
            {
                UserName = "xunittest",
                Email = "xunittest@2cs.com",
                Password = "AbC!123open",
                Role = "User"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/account/register-user")
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
