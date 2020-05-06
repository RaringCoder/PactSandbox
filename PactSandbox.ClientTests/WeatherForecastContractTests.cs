using System;
using PactSandbox.Client;
using Xunit;

namespace PactSandbox.ClientTests
{
    public class WeatherForecastContractTests : IClassFixture<WeatherForecastApiPact>
    {
        private readonly IVisitableMockProviderService _mockProviderService;
        private readonly WeatherForecastClient _client;


        public WeatherForecastContractTests(WeatherForecastApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions();
            _client = new WeatherForecastClient(data.MockProviderServiceBaseUri);
        }


        [Fact]
        public void WhenICallGetForecasts_ThenForecastsShouldBeReturned()
        {
            _mockProviderService.Accept(new WeatherForecastGetVisitor(GetExampleForecast()));

            var result = _client.GetForecasts();
            
            Assert.Equal(result.Length, 1);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void WhenICallPostForecast_WithMockForecast_ThenPostForecastShouldBeCalled()
        {
            var forecast = GetExampleForecast();

            _mockProviderService.Accept(new WeatherForecastPostVisitor(forecast));

            _client.PostForecast(1, forecast);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void WhenICallDeleteForecast_WithNoForecastToDelete_TheResultShouldBeFalse()
        {
            var forecastId = 1;

            _mockProviderService.Accept(new WeatherForecastDeleteNotFoundVisitor(forecastId));

            var result = _client.DeleteForecast(forecastId);

            Assert.False(result);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void WhenICallDeleteForecast_WithForecastToDelete_TheResultShouldBeTrue()
        {
            var forecastId = 1;

            _mockProviderService.Accept(new WeatherForecastDeleteOkVisitor(forecastId));

            var result = _client.DeleteForecast(forecastId);

            Assert.True(result);

            _mockProviderService.VerifyInteractions();
        }

        private static WeatherForecast GetExampleForecast() => new WeatherForecast
            {
                Date = DateTime.Now,
                Summary = "Warm",
                TemperatureC = 15
            };
    }
}
