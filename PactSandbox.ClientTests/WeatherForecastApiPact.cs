using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;

namespace PactSandbox.ClientTests
{
    public class WeatherForecastApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; }
        public IVisitableMockProviderService MockProviderService { get; }

        public int MockServerPort => 9000; 
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";


        public WeatherForecastApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig { SpecificationVersion = "2.0.0" }); //Configures the Specification Version
            
            PactBuilder
                .ServiceConsumer("Consumer")
                .HasPactWith("Weather Forecast API");

            MockProviderService = new MockProviderServiceDecorator(PactBuilder.MockService(MockServerPort, GetSerializerSettings()));
        }


        public void Dispose()
        {
            PactBuilder.Build(); // Outputs the pact file.
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
        }
    }
}
