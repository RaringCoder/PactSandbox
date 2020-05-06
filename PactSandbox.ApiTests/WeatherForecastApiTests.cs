using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace PactSandbox.ApiTests
{
    public class WeatherForecastApiTests : IAsyncDisposable
    {
        private readonly IHost _host;

        private readonly string _pactServiceUri;
        private readonly ITestOutputHelper _outputHelper;


        public WeatherForecastApiTests(ITestOutputHelper output)
        {
            _outputHelper = output;
            _pactServiceUri = "http://localhost:9001";

            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(_pactServiceUri);
                    webBuilder.UseStartup<ProviderStateStartupWrapper>();
                    webBuilder.UseKestrel();
                })
                .Build();

            _host.Start();
        }


        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_outputHelper)
                },
                PublishVerificationResults = true,
                Verbose = true,
                ProviderVersion = "2.0.0"
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState($"{_pactServiceUri}/provider-states")
                .ServiceProvider("Weather Forecast API", _pactServiceUri)
                .HonoursPactWith("Consumer")
                .PactUri(@"..\..\..\..\PactSandbox.ClientTests\pacts\consumer-weather_forecast_api.json")
                .Verify();
        }

        public async ValueTask DisposeAsync()
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
        }
    }
}
