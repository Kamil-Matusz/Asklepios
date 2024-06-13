using System.Net.Http.Headers;
using Asklepios.Application.Security;
using Asklepios.Application.Services.Clock;
using Asklepios.Core.DTO.Users;
using Asklepios.Infrastructure.Auth;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.Extensions.Options;

namespace Asklepios.IntegrationTests;

public abstract class BaseControllerTest : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; }
    private readonly IAuthenticator _authenticator;

    public BaseControllerTest(OptionsProvider optionsProvider)
    {
        var app = new AsklepiosTestApp();
        Client = app.Client;
        var postgresOptions = optionsProvider.Get<PostgresOptions>("postgres");
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), new Clock());
    }
    
    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }
}