using System;
using System.IO;

namespace FileLogger
{
    public class FileLogger
    {
        public static void Log(string filename, string msg)
        {
            File.AppendAllText(filename, $"{DateTime.Now} | {msg}\n");
        }
    }
}
