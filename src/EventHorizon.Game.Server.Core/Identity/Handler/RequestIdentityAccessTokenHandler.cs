namespace EventHorizon.Identity.Handler
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Identity.Exceptions;
    using IdentityModel.Client;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    public class RequestIdentityAccessTokenHandler : IRequestHandler<RequestIdentityAccessTokenEvent, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public RequestIdentityAccessTokenHandler(
            IConfiguration configuration,
            IHttpClientFactory clientFactory
        )
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public async Task<string> Handle(RequestIdentityAccessTokenEvent message, CancellationToken cancellationToken)
        {
            var tokenEndpoint = _configuration["Auth:Authority"];
            var clientId = _configuration["Auth:ClientId"];
            var clientSecret = _configuration["Auth:ClientSecret"];
            var apiScope = _configuration["Auth:ApiName"];
            // request token
            var tokenClient = new TokenClient(
                _clientFactory.CreateClient(),
                new TokenClientOptions
                {
                    Address = $"{tokenEndpoint}/connect/token",
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }
            );
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync(
                apiScope
            );

            if (tokenResponse.IsError)
            {
                throw new IdentityServerRequestException("Error requesting token.");
            }

            return tokenResponse.AccessToken;
        }
    }
}