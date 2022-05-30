using Blazored.LocalStorage;
using eCommerceApp.Server.UI.Providers;
using eCommerceApp.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace eCommerceApp.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, 
                                    AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(LoginUserDto loginMogel)
        {
          var response = await httpClient.LoginAsync(loginMogel);

            //Store Token
            await localStorage.SetItemAsync("authToken", response.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
