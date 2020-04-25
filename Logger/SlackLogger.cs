using SlackBotMessages;
using SlackBotMessages.Models;
using static Logger.Logger;

namespace Logger
{
    public class SlackLogger
    {
        private SbmClient webSlackBotClient;
        private SbmClient serverSlackBotClient;

        public SlackLogger(string webChannelUrl, string serverChannelUrl)
        {
            webSlackBotClient = new SbmClient(webChannelUrl);
            serverSlackBotClient = new SbmClient(serverChannelUrl);
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
        }

        private Message FormatMessage(string msg) =>
            new Message
            {
                Username = "Mohammed Ezzedine",
                Text = msg
            };
    }
}
