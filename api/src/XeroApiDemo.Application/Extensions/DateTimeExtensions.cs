namespace XeroApiDemo.Application.Extensions;

public static class DateTimeExtensions 
{
    public static bool OlderThanDays(this DateTime dateTime, int days) => 
        dateTime < DateTime.Now.AddDays(-days);
}
