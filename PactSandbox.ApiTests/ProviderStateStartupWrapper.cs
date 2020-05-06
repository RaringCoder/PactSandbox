using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace PactSandbox.ApiTests
{
    public class ProviderStateStartupWrapper
    {
        private readonly Startup _startup;


        public ProviderStateStartupWrapper(IConfiguration configuration)
        {
            _startup = new Startup(configuration);
        }


        public void ConfigureServices(IServiceCollection services)
        {
            _startup.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ProviderStateMiddleware>();

            _startup.Configure(app, env);
        }
    }

    public class ProviderState
    {
        public string Consumer { get; set; }
        public string State { get; set; }
    }
}
