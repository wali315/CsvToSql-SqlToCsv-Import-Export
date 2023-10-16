using System;
using System.IO;
using System.Configuration;
using ConsoleApp3;

internal class Program
{
    static void Main(string[] args)
    {
        MyConfiguration config = new MyConfiguration();
        var FilePath = config.GetFilePath();
        string connectionString = config.GetConnectionString(); // Get the connection string from your configuration
        string logPath = config.LogPath; // Get the log path from your configuration
        Logger logger = new Logger(logPath); // Create the logger

        // Import From Csv To Sql
        ////CsvToSql csvToSql = new CsvToSql(connectionString,logger);
        ////csvToSql.ImportCsvToSql(FilePath);

        // Export From Sql To Csv
        SqlToCsv csvToSql = new SqlToCsv(connectionString,logger);
        csvToSql.ExportSqlToCsv(FilePath);

        //Sending Email
        EmailSender emailSender= new EmailSender();
        emailSender.SendEmail();
    }
}
