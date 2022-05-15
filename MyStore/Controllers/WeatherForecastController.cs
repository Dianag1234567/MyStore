using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration config;
        private readonly IOptions<MySettings> appSettings; // store as it is
        private readonly MySettings mySettings; //strip it as IOptions interface -----------> mai usor de folosit

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IConfiguration config,
            IOptions<MySettings> appSettings)
        {
            _logger = logger;
            this.config = config;
            this.appSettings = appSettings;
            this.mySettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            //var openWeatherUrl = config.GetSection("MySettings").GetSection("OpenWeatherMapUrl").Value;
            //var apiKey = config.GetSection("MySettings").GetSection("ApiKey").Value;

            //v2
            //var openWeatherUrl = config.GetValue<string>("MySettings:OpenWeatherMapUrl");
            //var apiKey = config.GetValue<string>("MySettings:ApiKey");

            HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync($"{openWeatherUrl}{apiKey}");

            HttpResponseMessage response = await client.GetAsync($"{mySettings.OpenWeatherMapUrl}{mySettings.ApiKey}");
            //HttpResponseMessage response = await client.GetAsync($"{appSettings.Value.OpenWeatherMapUrl}{appSettings.Value.ApiKey}");

            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            var contentData = string.Empty;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                contentData = response.Content.ReadAsStringAsync().Result;
            }
            return Ok(contentData);
        }

    }
}
