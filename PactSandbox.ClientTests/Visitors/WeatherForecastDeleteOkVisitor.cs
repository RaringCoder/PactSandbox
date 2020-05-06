using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace PactSandbox.ClientTests
{
    public sealed class WeatherForecastDeleteOkVisitor : IMockProviderServiceVisitor
    {
        private readonly int _forecastId;


        public WeatherForecastDeleteOkVisitor(int forecastId)
        {
            _forecastId = forecastId;
        }


        public void Visit(IMockProviderService mockProvider)
        {
            var pathMatch = Matchers.Url($"/weatherforecast/1", "/weatherforecast/{number}");

            mockProvider
                .Given($"a forecast to delete with id '{_forecastId}'")
                .UponReceiving("a DELETE request to set a forecast")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Delete,
                    Path = pathMatch
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200
                });
        }
    }
}
