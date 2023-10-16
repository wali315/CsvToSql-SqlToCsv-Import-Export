using System;
using System.Configuration;
using System.IO;

public class MyConfiguration
{
    public string DirectoryPath { get; private set; }
    public string FileName { get; private set; }
    public string FilePath { get; private set; }
    public string LogPath { get; }

    public MyConfiguration()
    {
        DirectoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
        FileName = ConfigurationManager.AppSettings["FileName"];
        LogPath = ConfigurationManager.AppSettings["LogPath"];

        if (string.IsNullOrEmpty(DirectoryPath) || string.IsNullOrEmpty(FileName))
        {
            throw new ConfigurationErrorsException("DirectoryPath and FileName configuration values are missing or empty.");
        }

        FilePath = Path.Combine(DirectoryPath, FileName);
    }

    public string GetFilePath()
    {
        return FilePath;
    }

    public string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
}
