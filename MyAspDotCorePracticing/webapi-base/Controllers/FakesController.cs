using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyServices.FakedServices;

namespace webapi_base.Controllers
{
    [Route("api/fakes")]
    [ApiController]
    public class FakesController : ControllerBase
    {
        private readonly FakeServicePractice0 _fakeServicePractice0;
        private readonly IFakeServicePracticeByInterface _fakeServicePracticeByInterface;   
        private readonly IFakeServicePracticeByInterface2 _fakeServicePracticeByInterface2;
        private readonly IServiceProvider _serviceProvider;
        public FakesController(FakeServicePractice0 fakeServicePractice0, IFakeServicePracticeByInterface fakeServicePracticeByInterface, IFakeServicePracticeByInterface2 fakeServicePracticeByInterface2, IServiceProvider serviceProvider)
        {
            _fakeServicePractice0 = fakeServicePractice0;
            _fakeServicePracticeByInterface = fakeServicePracticeByInterface;
            _fakeServicePracticeByInterface2 = fakeServicePracticeByInterface2;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(
                new
                {
                   data0= _fakeServicePractice0.GetData(),
                   dataByInterface= _fakeServicePracticeByInterface.LogMessage(),
                   username=_fakeServicePracticeByInterface2.GetUserName(),
                });
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetUserName([FromServices] IFakeServicePracticeByInterface2 fakeServiceUser)
        {
            return Ok(
                new
                {
                    
                    username = fakeServiceUser.GetUserName(),
                });
        }

        [HttpGet("call-services/service-provider-resolve")]
        public async Task<IActionResult> GetUserNameServiceProvider([FromServices] IServiceProvider serviceProvider)
        {
            var fakedService = serviceProvider.GetService<IFakeServicePracticeByServiceProvider>();//??new FakeServicePracticeByServiceProvider();
            return Ok(
                new
                {

                    MessageFromServiceProvicer = fakedService.GetMessage(),
                });
        }

        [HttpGet("call-services/service-provider-resolve2")]
        public async Task<IActionResult> GetUserNameServiceProvider()
        {
            
            var fakedService = _serviceProvider.GetService<IFakeServicePracticeByServiceProvider>();//??new FakeServicePracticeByServiceProvider();
            return Ok(
                new
                {

                    MessageFromServiceProvicer = fakedService.GetMessage(),
                });
        }

        [HttpGet("call-services/request-services")]
        public async Task<IActionResult> GetUserNameServiceByRequestServices()
        {
            //IHttpContextAccessor httpContextAccessor;
            //httpContextAccessor.HttpContext.RequestServices.GetService<IFakeServicePracticeByServiceProvider>();

            var serviceResovledByContextRequestServiced= HttpContext.RequestServices.GetService<IFakeServicePracticeByServiceProvider>();
            return Ok(
                new
                {

                    MessageFromServiceProvicer = serviceResovledByContextRequestServiced.GetMessage(),
                });
        }

    }
}
