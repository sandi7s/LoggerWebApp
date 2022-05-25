using System.Threading.Tasks;
using Logger.Models.TokenAuth;
using Logger.Web.Controllers;
using Shouldly;
using Xunit;

namespace Logger.Web.Tests.Controllers
{
    public class HomeController_Tests: LoggerWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}