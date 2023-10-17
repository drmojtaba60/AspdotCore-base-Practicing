namespace MyServices.Tools.IPChecker
{
    public enum IPCheckerResponseStatus
    {
        None = 0,
        IsInRange,
        IsNoValidClientIp,
        IsNoValidConfig,
        IsNotInRange
    }

}
