using Discord;
using Microsoft.Extensions.Logging;

namespace AiBot.Core;

public static class LogExtensions
{
    public static void Log(this ILogger logger, LogMessage logMessage)
    {
        var messageContent = logMessage.Message;
        logger.Log(logMessage.Severity.ToLogLevel(), logMessage.Exception, messageContent);
    }

    private static LogLevel ToLogLevel(this LogSeverity severity)
    {
        return severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Trace,
            LogSeverity.Debug => LogLevel.Debug,
            _ => LogLevel.None
        };
    }
}