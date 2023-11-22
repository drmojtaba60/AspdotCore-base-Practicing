using Microsoft.Extensions.Logging;

namespace MyServices.FakedServices
{
    public class FakeServicePractice0
    {
        public FakeServicePractice0()
        {
            
        }
        public string GetData() => $"Hi Im FakeSericePractice0,{DateTime.Now}";
    }




    public interface IFakeServicePracticeByInterface
    {
        string LogMessage();
    }
    public class FakeServicePracticeByInterface : IFakeServicePracticeByInterface
    {
        private readonly ILogger _logger;

        public FakeServicePracticeByInterface(ILogger<FakeServicePracticeByInterface> logger)
        {
            _logger = logger;
        }

        public string LogMessage()
        {
            _logger.LogInformation("Hi This is FakeServicePracticeByInterface");
            return "data is logged";
        }
    }
    public interface IFakeServicePracticeByInterface2
    {
        string GetUserName();
    }
    public class FakeServicePracticeByInterface2 : IFakeServicePracticeByInterface2
    {
        private string _userName;

        public FakeServicePracticeByInterface2(string userName)
        {
            _userName = userName;
        }

      
        public string GetUserName()
        {
           
            return $"UserName is {_userName}";
        }
    }
    public interface IFakeServicePracticeByServiceProvider
    {
        string GetMessage();
    }
    public class FakeServicePracticeByServiceProvider : IFakeServicePracticeByServiceProvider
    {
        private string _userName;

        public FakeServicePracticeByServiceProvider()
        {
            _userName = "Hi ServiceProvider Tanks for Resolve";
        }


        public string GetMessage()
        {

            return $"UserName is {_userName}";
        }
    }
}
