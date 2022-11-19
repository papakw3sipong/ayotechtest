using aYoTechTest.BR.ViewModels;

namespace aYoTechTest.ApiTest
{
    public class IdentityControllerTest : aYoTechTestBase
    {
        [Fact]
        public async Task A_WhenCalled_ReturnAccessToken()
        {
            //Act
            ApiAuthRequest _tokenRequest = new ApiAuthRequest()
            {
                Password = _testUserPass,
                Username = _testUserName
            };

            var _tokenResult = await _identityController.AuthenticateUser(_tokenRequest);
            Assert.NotNull(_tokenResult);
            var _token = Assert.IsType<ApiAuthResponse>(_tokenRequest);
            Assert.True(_token.FullName.Equals(_testUserName));
            Assert.True(!string.IsNullOrEmpty(_token.Token));

        }
    }
}
