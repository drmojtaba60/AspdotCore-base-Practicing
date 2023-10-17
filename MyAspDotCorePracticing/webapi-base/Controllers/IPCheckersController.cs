using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServices.Tools.IPChecker;

namespace webapi_base.Controllers
{
    [Route("api/checking/ip")]
    [ApiController]
    public class IPCheckersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IIPCsisCheckerService _ipcsisCheckerService;

        public IPCheckersController(ILogger<IPCheckersController> logger, IIPCsisCheckerService ipcsisCheckerService)
        {
            _logger = logger;
            _ipcsisCheckerService = ipcsisCheckerService;
        }
        [HttpGet]
        public async Task<IActionResult> IsValid()
        {
            var isValid = _ipcsisCheckerService.IsValid();
            return Ok(new
            {
                isValid,
                _ipcsisCheckerService.Status,
                StatusToString= _ipcsisCheckerService.Status.ToString(),
                _ipcsisCheckerService.StatusTitle
            });
        }
    }
}
