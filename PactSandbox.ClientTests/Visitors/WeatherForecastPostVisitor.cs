using System.Collections.Generic;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace PactSandbox.ClientTests
{
    public class WeatherForecastPostVisitor : IMockProviderServiceVisitor
    {
        private readonly WeatherForecast _exampleForecast;


        public WeatherForecastPostVisitor(WeatherForecast exampleForecast)
        {
            _exampleForecast = exampleForecast;
        }


        public void Visit(IMockProviderService mockProvider)
        {
            var pathMatch = Matchers.Url($"/weatherforecast/1", "/weatherforecast/{number}");

            mockProvider
                .UponReceiving("a POST request to set a forecast")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = pathMatch,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        date = Match.Type(_exampleForecast.Date),
                        summary = Match.Type(_exampleForecast.Summary),
                        temperatureC = Match.Type(_exampleForecast.TemperatureC),
                        temperatureF = Match.Type(_exampleForecast.TemperatureF)
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200
                });
        }
    }
}
