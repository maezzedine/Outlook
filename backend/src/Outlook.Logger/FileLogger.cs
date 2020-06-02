using System.IO;
using static Outlook.Logger.Logger;

namespace Outlook.Logger
{
    public class FileLogger
    {
        private string webFileName;
        private string serverFileName;

        public FileLogger(string webFileName, string serverFileName)
        {
            this.webFileName = webFileName;
            this.serverFileName = serverFileName;
        }

        public void Log(LogField logField, string msg)
        {
            if (logField == LogField.web)
            {
                File.AppendAllText(webFileName, msg);
            }
            if (logField == LogField.server)
            {
                File.AppendAllText(serverFileName, msg);
            }
        }
    }
}
