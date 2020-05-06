using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PactSandbox.Models;

namespace PactSandbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = ((WeatherSummary)rng.Next(0, 7)).ToString()
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public WeatherForecast Get(int id)
        {
            var json = System.IO.File.ReadAllText($"forecast_{id}.json");
            return JsonSerializer.Deserialize<WeatherForecast>(json);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult Post(int id, [FromBody] WeatherForecast forecast)
        {
            var json = JsonSerializer.Serialize(forecast);
            System.IO.File.WriteAllText($"forecast_{id}.json", json);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var path = $"forecast_{id}.json";

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok();
            }

            return NotFound();
        }
    }
}
