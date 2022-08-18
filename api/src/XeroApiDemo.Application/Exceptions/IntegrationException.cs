namespace XeroApiDemo.Application.Exceptions;

public class IntegrationException : InvalidOperationException
{
    public IntegrationException(string message) : base(message) { }
}
