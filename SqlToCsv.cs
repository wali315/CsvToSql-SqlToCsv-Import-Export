using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using ConsoleApp3;

public class SqlToCsv
{
    private readonly string _connectionString;
    private readonly Logger _logger;

    public SqlToCsv(string connectionString, Logger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public void ExportSqlToCsv(string csvFilePath)
    {
        try
        {
            using (var context = new CsvDbContext())
            {
                var records = context.CsvModels.ToList();

                using (var writer = new StreamWriter(csvFilePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(records);
                }
            }

            Console.WriteLine("Data exported from SQL to CSV.");
            _logger.Log("Csv data Exported to SQL successfully.");

        }
        catch (Exception ex)
        {
            _logger.Log($"An error occurred: {ex.Message}");
        }
    }
}
