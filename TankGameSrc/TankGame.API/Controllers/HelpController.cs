using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankGame.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TankGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private IConfiguration _configuration;
        public HelpController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpGet(Name = "GetInfo")]
        public  string GetInfo()
        {
            
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? _configuration["Data:DbContext:GameConnectionString"];
            var strInfo = $"Check api calls at server time { DateTime.Now.ToShortTimeString()} with connection string {connectionString}";

            return strInfo;
        }
    }
}
