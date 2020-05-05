using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace PactSandbox.Client
{
    public class WeatherForecastClient
    {
        private readonly HttpClient _client;


        public WeatherForecastClient(string baseUri = null)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://localhost:5000") };
        }


        public WeatherForecast[] GetForecasts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/weatherforecast");
            request.Headers.Add("Accept", "application/json");

            var response = _client.SendAsync(request);

            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            var reasonPhrase = response.Result.ReasonPhrase;

            request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content) ?
                    JsonConvert.DeserializeObject<WeatherForecast[]>(content)
                    : null;
            }

            throw new Exception(reasonPhrase);
        }

        public void PostForecast(int forecastId, WeatherForecast forecast)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/weatherforecast/{forecastId}");

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string json = JsonConvert.SerializeObject(forecast, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.SendAsync(request).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
