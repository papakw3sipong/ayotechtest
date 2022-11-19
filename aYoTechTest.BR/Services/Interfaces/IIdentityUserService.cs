using aYoTechTest.BR.ViewModels;

namespace aYoTechTest.BR.Services.Interfaces
{
    public interface IIdentityUserService

    {
        Task<ApiAuthResponse> AuthenticateUser(ApiAuthRequest model);
    }
}
