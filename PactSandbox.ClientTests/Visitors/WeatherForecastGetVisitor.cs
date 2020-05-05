using System.Collections.Generic;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace PactSandbox.ClientTests
{
    public class WeatherForecastGetVisitor : IMockProviderServiceVisitor
    {
        private readonly WeatherForecast _exampleForecast;


        public WeatherForecastGetVisitor(WeatherForecast exampleForecast)
        {
            _exampleForecast = exampleForecast;
        }


        public void Visit(IMockProviderService mockProvider)
        {
            mockProvider
                .UponReceiving("a GET request to retrieve the forecasts")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/weatherforecast",
                    Headers = new Dictionary<string, object> { { "Accept", "application/json" } }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object> { { "Content-Type", "application/json; charset=utf-8" } },
                    Body = Match.MinType(
                        new
                        {
                            date = Match.Type(_exampleForecast.Date),
                            summary = Match.Type(_exampleForecast.Summary),
                            temperatureC = Match.Type(_exampleForecast.TemperatureC)
                        }, 
                        1)
                });
        }
    }
}