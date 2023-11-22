using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace MyServices.Tools.IPChecker
{
    public class IPCsisCheckerService : IIPCsisCheckerService
    {
        private string _rangeCsisIPStart;
        private string _rangeCsisIPEnd;
        private string _clientIP;
        public string ErrorMessage { get; set; }
        public IPCheckerResponseStatus Status { get; set; }
        public IPCsisCheckerService(IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            Status = IPCheckerResponseStatus.None;
            _rangeCsisIPStart = config["IPOrganRange:Start"] ?? "";
            _rangeCsisIPEnd = config.GetSection("IPOrganRange:End").Value ?? "";
            _clientIP = getClientIP(contextAccessor);
        }
        public string StatusTitle => Enum.GetName(Status.GetType(), Status);
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_clientIP))
            {
                Status = IPCheckerResponseStatus.IsNoValidClientIp;
                return false;
            }
            if (string.IsNullOrEmpty(_rangeCsisIPStart) || string.IsNullOrEmpty(_rangeCsisIPEnd))
            {
                Status = IPCheckerResponseStatus.IsNoValidConfig;
                return false;
            }

            var ipClientAsLong = ConvertToLong(_clientIP);
            var ipStartAsLong = ConvertToLong(_rangeCsisIPStart);
            var ipEndAsLong = ConvertToLong(_rangeCsisIPEnd);
            if (ipClientAsLong <= 0)
            {
                Status = IPCheckerResponseStatus.IsNoValidClientIp;
                return false;
            }
            if (ipStartAsLong <= 0 || ipEndAsLong <= 0)
            {
                Status = IPCheckerResponseStatus.IsNoValidConfig;
                return false;
            }
            var result = ipClientAsLong >= ipStartAsLong && ipClientAsLong <= ipEndAsLong;
            Status = result ? IPCheckerResponseStatus.IsInRange : IPCheckerResponseStatus.IsNoValidConfig;
            return result;
        }
        private long ConvertToLong(string ipAddress)
        {
            if (!ipAddress.Contains("."))
            {
                ErrorMessage = $"{ipAddress} is not valid";
                return 0;
            }
            if (!Regex.IsMatch(ipAddress, @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"))
            {
                ErrorMessage = $"{ipAddress} is not valid";
                return 0;
            }
            var ipClientArray = ipAddress.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var resultString = "";
            foreach (var item in ipClientArray)
            {
                if (item.Length == 1) resultString += $"00{item}";
                else if (item.Length == 2) resultString += $"0{item}";
                else resultString += $"{item}";
            }
            long result = 0;
            long.TryParse(resultString, out result);
            return result;
        }

        private string getClientIP(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor?.HttpContext?.Request;
            string headerIP = request?.Headers?["X-FORWARDED-FOR"].FirstOrDefault() ?? "";
            headerIP = headerIP == "" ? request?.Headers?["HTTP_X_FORWARDED_FOR"].FirstOrDefault() ?? "" : headerIP;
            headerIP = headerIP == "" ? request?.Headers?["REMOTE_ADDR"].FirstOrDefault() ?? "" : headerIP;
            var remoteIpAddress = request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
            headerIP = string.IsNullOrEmpty(headerIP) ? remoteIpAddress : headerIP;
            if (string.IsNullOrEmpty(headerIP) || !System.Net.IPAddress.TryParse(headerIP, out System.Net.IPAddress ip))
                headerIP = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";
            return headerIP;
        }
    }

}
