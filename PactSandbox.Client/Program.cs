using System;
using System.Net.Http;

namespace PactSandbox.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WeatherForecastClient();

            var forecasts = client.GetForecasts();

            client.PostForecast(1, forecasts[0]);

            Console.WriteLine("Forecast received.");
            Console.Read();
        }
    }
}
