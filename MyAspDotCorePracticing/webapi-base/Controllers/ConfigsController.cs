using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using webapi_base.Models;

namespace webapi_base.Controllers
{
    [ApiController]
    [Route("configs")]
    public class ConfigsController : ControllerBase
    {

        private readonly ILogger<ConfigsController> _logger;
        private readonly IConfiguration _config;

        public ConfigsController(ILogger<ConfigsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

     

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Description = _config.GetValue<string>("Description"),
                Description2 = _config["Description"],//as string?
                IsDevelopingMode = _config["IsDevelopingMode"],//as string
                IsDevelopingModeAsBool = _config.GetValue<bool>("IsDevelopingMode"),//convert to type
                IsDevelopingModeAsBoolNotFund = _config["IsDevelopingMode0"]??"",//is not found set null
                IsDevelopingModeAsBoolNotFund1 = _config.GetValue<bool>("IsDevelopingMode0"),//is not found set defual value =false
                DeveloperNull = _config["Developer"],//incrroct way for get complex object
                DeveloperMail = _config.GetSection("Developer:Mail").Value ,
                DeveloperMail2 = _config.GetSection("Developer").GetValue<string>("Mail"),
                DeveloperFullName = _config["Developer:FullName"],
                BrithDate = _config["Developer:BrithDate"],
                BrithDateAsDateTime = _config.GetSection("Developer").GetValue<DateTime>("BrithDate"),
                BrithDateAsDateTime2 = _config.GetValue<DateTime>("Developer:BrithDate"),
                BrithDateAsDateTimeByConvertMethod = Convert.ToDateTime(_config["Developer:BrithDate"]),

            });
        }
        [HttpGet("options/developer")]
        public async Task<IActionResult> GetDeveloper([FromServices]IOptions<Developer> developer)
        {
            //the dependency injection service container. must set config service in program.cs
            var developerConfig =await Task.Factory.StartNew(() => developer.Value);
            return Ok(developerConfig);
        }
        [HttpGet("options/developer-get")]
        public async Task<IActionResult> GetDeveloperGetGeneric()
        {
            var developerBind = _config.GetSection("DeveloperBind").Get< Developer >();
            return Ok(await Task.FromResult(developerBind));
        }
        [HttpGet("options/developer-bind")]
        public async Task<IActionResult> GetDeveloperBind()
        {
            var developerBind = new Developer();
            _config.GetSection("DeveloperBind").Bind(developerBind);
            return Ok(await Task.FromResult(developerBind));
        }
    }
}