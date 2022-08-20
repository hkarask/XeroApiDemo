namespace XeroApiDemo.Application.Accounts;

public record AccountCheckResult
{
    public bool IsValid { get; init; }

    public string Message { get; init; }

    public static AccountCheckResult Passed() =>
        new()
        {
            IsValid = true
        };

    public static AccountCheckResult Failed(string message) =>
        new()
        {
            IsValid = false, 
            Message = message
        };
}
