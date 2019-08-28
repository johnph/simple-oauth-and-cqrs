namespace Sample.WebApp.Helpers
{
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class SecurityTokenHelper : ISecurityTokenHelper
    {
        private readonly AuthToken _authToken;
        private readonly ISession _session;

        public SecurityTokenHelper(IOptions<AuthToken> options, ISession session)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _authToken = options.Value;
            _session = session;
        }

        public async Task<string> GetSessionToken()
        {
            var sessionAuthToken = _session.GetString($"{_session.Id}-authtoken");

            if (sessionAuthToken == null)
            {
                var authResult = await RequestTokenAsync();

                if (authResult != null)
                {
                    sessionAuthToken = authResult.AccessToken;
                    _session.SetString($"{_session.Id}-authtoken", sessionAuthToken);
                }

                throw new InvalidOperationException();
            }

            return sessionAuthToken;
        }

        public async Task<string> GetAccessToken()
        {
            var result = await RequestTokenAsync();

            return result != null ? 
                result.AccessToken :
                throw new InvalidOperationException(result.ErrorDescription);
        }

        private async Task<TokenResponse> RequestTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var disco = await client.GetDiscoveryDocumentAsync(_authToken.Authority);

                if (disco.IsError) throw new Exception(disco.Error);

                var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = _authToken.ClientId,
                    ClientSecret = _authToken.ClientSecret,
                    Scope = _authToken.Scope
                });

                if (response.IsError) throw new Exception(response.Error);
                return response;
            }
        }
    }
}
