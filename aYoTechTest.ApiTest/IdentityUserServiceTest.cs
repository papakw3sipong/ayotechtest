using aYoTechTest.BR.ViewModels;
using aYoTechTest.Models.Entities.Identity;
using aYoTechTest.Services.Classes;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace aYoTechTest.ApiTest
{

    public class IdentityUserServiceTest
    {




        [Fact]
        public async Task Test_Generate_AccessToken()
        {
            //Act
            var ayoTestBase = new aYoTechTestBase();

            var store = new Mock<IUserStore<aYoTechTestUser>>();
            var mgr = new Mock<UserManager<aYoTechTestUser>>(store.Object, null, null, null, null, null, null, null, null);

            store.Setup(x => x.FindByIdAsync(It.IsAny<string>(), CancellationToken.None))
             .ReturnsAsync(new aYoTechTestUser()
             {
                 UserName = ayoTestBase._testUserName,
                 Id = ayoTestBase._testUserId,
                 Email = "test@example.com"
             });

            await mgr.Object.CreateAsync(ayoTestBase.TestUser);

            var _identityService = new IdentityUserService(mgr.Object, ayoTestBase._apiSetting);

            ApiAuthRequest _tokenRequest = new ApiAuthRequest()
            {
                Password = ayoTestBase._testUserPass,
                Username = ayoTestBase._testUserName,
            };

            //await ayoTestBase._userManager.CreateAsync(ayoTestBase.TestUser, ayoTestBase._testUserPass);

            var _tokenResult = await _identityService.AuthenticateUser(_tokenRequest);
            Assert.NotNull(_tokenResult);
            var _token = Assert.IsType<ApiAuthResponse>(_tokenRequest);
            Assert.True(_token.FullName.Equals(ayoTestBase._testUserName));
            Assert.True(!string.IsNullOrEmpty(_token.Token));

        }
    }
}
