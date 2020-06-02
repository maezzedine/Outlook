using SlackBotMessages;
using SlackBotMessages.Models;
using static Outlook.Logger.Logger;

namespace Outlook.Logger
{
    public class SlackLogger
    {
        private SbmClient webSlackBotClient;
        private SbmClient serverSlackBotClient;
        private SbmClient userArticlesSlackBotClient;

        public SlackLogger(string webChannelUrl, string serverChannelUrl, string userArticlesChannelUrl)
        {
            webSlackBotClient = new SbmClient(webChannelUrl);
            serverSlackBotClient = new SbmClient(serverChannelUrl);
            userArticlesSlackBotClient = new SbmClient(userArticlesChannelUrl);
        }

        public void Log(LogField logField, string msg)
        {
            if (logField == LogField.web)
            {
                webSlackBotClient.Send(FormatMessage(msg));
            }
            if (logField == LogField.server)
            {
                serverSlackBotClient.Send(FormatMessage(msg));
            }
            if (logField == LogField.userArticles)
            {
                userArticlesSlackBotClient.Send(FormatMessage(msg));
            }
        }

        private Message FormatMessage(string msg) =>
            new Message
            {
                Username = "Article By User",
                Text = msg
            };
    }
}
