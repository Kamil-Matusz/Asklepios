using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

namespace Asklepios.IntegrationTests;

internal sealed class AsklepiosTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public AsklepiosTestApp()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("test");
        }).CreateClient();

    }
}