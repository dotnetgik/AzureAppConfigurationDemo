using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureAppConfigurationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppConfigurationDemo : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AppConfigurationDemo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetConfigurationValue")]
        public IActionResult GetConfigurationValue()
        {

            var demoSetting = _configuration["demotest:testsettings"];
            var demoSetting1 = _configuration["demotest:testsettings1"];
            
            return Ok(new List<string>{demoSetting,demoSetting1});

        }
    }
}
