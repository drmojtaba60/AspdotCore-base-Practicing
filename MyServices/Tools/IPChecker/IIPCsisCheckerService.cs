namespace MyServices.Tools.IPChecker
{
    public interface IIPCsisCheckerService
    {
        string ErrorMessage { get; set; }
        bool IsValid();
        public IPCheckerResponseStatus Status { get; set; }
        string StatusTitle { get; }
    }

}
