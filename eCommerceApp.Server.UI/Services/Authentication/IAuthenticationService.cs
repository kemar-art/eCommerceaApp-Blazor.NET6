using eCommerceApp.Server.UI.Services.Base;

namespace eCommerceApp.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginMogel);

        public Task Logout();
    }
}
