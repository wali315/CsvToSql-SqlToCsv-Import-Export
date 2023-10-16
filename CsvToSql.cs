using System;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3
{
    public class CsvToSql
    {
        private readonly string _connectionString;
        private readonly Logger _logger;


        public CsvToSql(string connectionString, Logger logger)
        {
            _connectionString = connectionString;
            _logger = logger;

        }

        public void ImportCsvToSql(string csvFilePath)
        {
            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<CsvModel>().ToList();

                    using (var context = new CsvDbContext())
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var record in records)
                            {
                                var existingPerson = context.CsvModels.FirstOrDefault(p => p.Id == record.Id);

                                if (existingPerson != null)
                                {
                                    // If the ID exists, update the record
                                    existingPerson.Name = record.Name;
                                    existingPerson.Age = record.Age;
                                    existingPerson.Country = record.Country;
                                }
                                else
                                {
                                    // If the ID doesn't exist, add a new record
                                    context.CsvModels.Add(new CsvModel
                                    {
                                        Name = record.Name,
                                        Age = record.Age,
                                        Country = record.Country
                                    }); // Do not set the Id property explicitly
                                }
                            }

                            context.SaveChanges();
                            transaction.Commit();
                            Console.WriteLine("Data Imported from CSV To SQL.");
                            _logger.Log("Csv data imported to SQL successfully.");
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions and log errors
                            _logger.Log($"An error occurred during database operations: {ex.Message}");

                            if (ex.InnerException != null)
                            {
                                _logger.Log($"Inner Exception: {ex.InnerException.Message}");
                            }

                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to file processing
                _logger.Log($"An error occurred during CSV processing: {ex.Message}");
            }
        }
    }
}
