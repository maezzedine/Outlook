using System;

namespace Logger
{
    public class Logger
    {
        private static Logger webLogger;
        private static Logger serverLogger;
        private static Logger userArticlesLogger;

        private FileLogger fileLogger;
        private SlackLogger slackLogger;

        private LogField logField;

        private Logger(LogField logField) 
        {
            fileLogger = new FileLogger(Config.WebFileName, Config.ServerFileName);
            slackLogger = new SlackLogger(Config.WebSlackChannel, Config.ServerSlackChannel, Config.UserArticlesSlackChannel);
            this.logField = logField;
        }

        public static Logger Instance(LogField logField)
        {
            if (logField == LogField.web)
            {
                if (webLogger == null)
                {
                    webLogger = new Logger(LogField.web);
                }
                return webLogger;
            }
            else if (logField == LogField.userArticles)
            {
                if (userArticlesLogger == null)
                {
                    userArticlesLogger = new Logger(LogField.userArticles);
                }
                return userArticlesLogger;
            }
            else
            {
                if (serverLogger == null)
                {
                    serverLogger = new Logger(LogField.server);
                }
                return serverLogger;
            }
        }

        public void Log(string message)
        {
            var msg = MessageFormat(message);
            fileLogger.Log(logField, msg);
            slackLogger.Log(logField, msg);
        }

        public enum LogField { server, web, userArticles }

        private static string MessageFormat(string msg) => $"{DateTime.Now} | {msg}\n";
    }
}
