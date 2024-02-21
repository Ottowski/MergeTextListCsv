﻿using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

// Define classes to represent the structure of your CSV files
public class FirstCsvRecord
{
    [Name("AdsVariableName")]
    public string AdsVariableName { get; set; }

    [Name(" ModbusAddress")] // Notice the space before ModbusAddress
    public int ModbusAddress { get; set; }

    [Name(" ModbusPermission")] // Notice the space before ModbusPermission
    public string ModbusPermission { get; set; }
}

public class SecondCsvRecord
{
    [Name("AdsVariableName")]
    public string AdsVariableName { get; set; }

    [Name(" Type")] // Notice the space before Type
    public string Type { get; set; }
}

public class CombinedCsvRecord
{
    public string AdsVariableName { get; set; }
    public int? ModbusAddress { get; set; }
    public string ModbusPermission { get; set; }
    public string Type { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to CSV File Merger!");

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Merge CSV files");
            Console.WriteLine("2. Exit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MergeCsvFiles();
                    break;
                case "2":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void MergeCsvFiles()
    {
        string baseDirectory = @"C:\Users\ottoa\OneDrive\Skrivbord\MergeTextListCsv\MergeTextListCsv\MergeTextListCsv\bin\Debug\net8.0";
        string firstCsvFilePath = Path.Combine(baseDirectory, "example-input-list-without-indexes.csv");
        string secondCsvFilePath = Path.Combine(baseDirectory, "example-input-list-another-input.csv");
        string combinedCsvFilePath = Path.Combine(baseDirectory, "trying-to-be-perfect-combined-List.csv");

        // Read both CSV files
        List<FirstCsvRecord> firstCsvRecords;
        List<SecondCsvRecord> secondCsvRecords;

        using (var reader = new StreamReader(firstCsvFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            firstCsvRecords = csv.GetRecords<FirstCsvRecord>().ToList();
        }

        using (var reader = new StreamReader(secondCsvFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            secondCsvRecords = csv.GetRecords<SecondCsvRecord>().ToList();
        }

        // Merge the data
        var combinedRecords = new List<CombinedCsvRecord>();

        // Merge records first CSV file
        foreach (var firstRecord in firstCsvRecords)
        {
            var combinedRecord = new CombinedCsvRecord
            {
                AdsVariableName = firstRecord.AdsVariableName,
                ModbusAddress = firstRecord.ModbusAddress,
                ModbusPermission = firstRecord.ModbusPermission
            };
            combinedRecords.Add(combinedRecord);
        }

        // Add missing records first CSV file
        foreach (var firstRecord in firstCsvRecords)
        {
            if (!combinedRecords.Any(x => x.AdsVariableName == firstRecord.AdsVariableName))
            {
                var combinedRecord = new CombinedCsvRecord
                {
                    AdsVariableName = firstRecord.AdsVariableName,
                    ModbusAddress = firstRecord.ModbusAddress,
                    ModbusPermission = firstRecord.ModbusPermission
                };
                combinedRecords.Add(combinedRecord);
            }
        }

        // Merge records second CSV file
        foreach (var secondRecord in secondCsvRecords)
        {
            // Check if the record already exists in the combined list
            if (!combinedRecords.Any(x => x.AdsVariableName == secondRecord.AdsVariableName))
            {
                // If not, create a new CombinedCsvRecord with null values for ModbusAddress and ModbusPermission
                var combinedRecord = new CombinedCsvRecord
                {
                    AdsVariableName = secondRecord.AdsVariableName,
                    Type = secondRecord.Type
                };
                combinedRecords.Add(combinedRecord);
            }
        }

        // Add missing records second CSV file
        foreach (var secondRecord in secondCsvRecords)
        {
            if (!combinedRecords.Any(x => x.AdsVariableName == secondRecord.AdsVariableName))
            {
                var combinedRecord = new CombinedCsvRecord
                {
                    AdsVariableName = secondRecord.AdsVariableName,
                    Type = secondRecord.Type
                };
                combinedRecords.Add(combinedRecord);
            }
        }

        // Write the combined new CSV file
        using (var writer = new StreamWriter(combinedCsvFilePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(combinedRecords);
        }

        Console.WriteLine("Combined CSV file created successfully.");
    }
}
