/*using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Globalization;

public class Program
{
    static void Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the NewTextMerger");
            Console.WriteLine("\n1. Generate a new list");
            Console.WriteLine("2. Generate a list with separated indexes");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            string MenuOption = Console.ReadLine();

            switch (MenuOption)
            {
                case "1":
                    Console.WriteLine("Creating a new list ...");
                    GenerateNewList();
                    Console.WriteLine("List successfully created!");
                    Console.WriteLine("Press Enter to return to the main menu...");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Creating a new index list ...");
                    IndexList();
                    Console.WriteLine("Index list 'indexList.csv' created successfully.");
                    Console.WriteLine("Press Enter to return to the main menu...");
                    Console.ReadLine(); // Wait for user to press Enter before returning to the main menu
                    break;
                case "0":
                    keepRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("Press any of the number options to continue ...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void GenerateNewList()
    {
        var projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;

        var oldListFile1 = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        var oldListFile2 = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        var newListFile = Path.Combine(projectFolderPath, "imprefect-combined-List.csv");

        // Read CSV files using CsvHelper
        var records1 = ReadCsv<DataItem>(oldListFile1);
        var records2 = ReadCsv<DataItem>(oldListFile2);

        // Combine records from both files
        var combinedRecords = records1.Concat(records2).GroupBy(r => r.AdsVariableName)
                                                  .Select(g => g.First())
                                                  .ToList();

        // Write combined data to a new CSV file
        WriteCsv(newListFile, combinedRecords);
        Console.WriteLine($"New list file '{newListFile}' created successfully with {combinedRecords.Count} entries.");
    }

    private static void IndexList()
    {
        var projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        var anotherOldFile1 = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        var anotherOldFile2 = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        var newfile = Path.Combine(projectFolderPath, "indexList.csv");

        // Read CSV files using CsvHelper
        var records1 = ReadCsv<DataItem>(anotherOldFile1);
        var records2 = ReadCsv<DataItem>(anotherOldFile2);

        // Combine records from both files
        var combinedRecords = records1.Concat(records2).GroupBy(r => r.AdsVariableName)
                                                  .Select(g => new DataItem
                                                  {
                                                      AdsVariableName = g.Key,
                                                      ModbusAddress = g.First().ModbusAddress,
                                                      ModbusPermission = g.First().ModbusPermission,
                                                      Type = g.First().Type
                                                  })
                                                  .ToList();

        // Write combined data to a new CSV file
        WriteCsv(newfile, combinedRecords);
        Console.WriteLine($"Index list file '{newfile}' created successfully with {combinedRecords.Count} entries.");
    }

    private static List<T> ReadCsv<T>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Read the headers
        csv.Read();
        csv.ReadHeader();

        // Get the headers
        var headers = csv.HeaderRecord;

        // Trim and convert header names to lowercase for matching
        var preparedHeaders = headers.Select(header => header.Trim().ToLower(CultureInfo.InvariantCulture)).ToArray();

        return csv.GetRecords<T>().ToList();
    }





    private static void WriteCsv<T>(string filePath, IEnumerable<T> records)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

        csv.WriteRecords(records);
    }

    public class DataItem
    {
        public string AdsVariableName { get; set; }
        public string ModbusAddress { get; set; }
        public string ModbusPermission { get; set; }
        public string Type { get; set; }
    }
}
*/