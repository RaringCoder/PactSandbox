using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace PactSandbox.ClientTests
{
    public sealed class WeatherForecastDeleteNotFoundVisitor : IMockProviderServiceVisitor
    {
        private readonly int? _forecastId;


        public WeatherForecastDeleteNotFoundVisitor(int forecastId)
        {
            _forecastId = forecastId;
        }


        public void Visit(IMockProviderService mockProvider)
        {
            var pathMatch = Matchers.Url($"/weatherforecast/1", "/weatherforecast/{number}");

            mockProvider
                .Given($"no forecast with id '{_forecastId}'")
                .UponReceiving("a DELETE request to set a forecast")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Delete,
                    Path = pathMatch
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 404
                });
        }
    }
}