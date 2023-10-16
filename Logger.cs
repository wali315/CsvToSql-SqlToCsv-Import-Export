using System;
using System.IO;

public class Logger
{
    private readonly string logPath;

    public Logger(string logPath)
    {
        this.logPath = logPath;
    }

    public void Log(string message)
    {
        string logEntry = $"{DateTime.Now}: {message}";

        using (StreamWriter sw = File.AppendText(logPath))
        {
            sw.WriteLine(logEntry);
        }
    }
}
