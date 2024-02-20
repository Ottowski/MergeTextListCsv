
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;

public class Record
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CsvReaderExample
{
    public void ReadCsvFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<Record>();
        foreach (var record in records)
        {
            Console.WriteLine($"Id: {record.Id}, Name: {record.Name}, Description: {record.Description}");
        }
    }
}


class Program
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

        string[] oldList1 = File.ReadAllLines(oldListFile1);
        string[] oldList2 = File.ReadAllLines(oldListFile2);

        string[] mergedList = new string[oldList1.Length + oldList2.Length];
        Array.Copy(oldList1, 0, mergedList, 0, oldList1.Length);
        Array.Copy(oldList2, 0, mergedList, oldList1.Length, oldList2.Length);

        File.WriteAllLines(newListFile, mergedList);

        Console.WriteLine($"New list file '{newListFile}' created successfully with {mergedList.Length} entries.");
    }

    public class DataItem
    {
        public string AdsVariableName { get; set; }
        public string ModbusAddress { get; set; }
        public string ModbusPermission { get; set; }
        public string Type { get; set; }
    }

    private static void IndexList()
    {
        var projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        var anotherOldFile1 = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        var anotherOldFile2 = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        var newfile = Path.Combine(projectFolderPath, "indexList.csv");

        var anotherInputDictionary = new Dictionary<string, DataItem>();
        var withoutIndexesDictionary = new Dictionary<string, DataItem>();

        ReadDataFromFile(anotherOldFile1, anotherInputDictionary);
        ReadDataFromFile(anotherOldFile2, withoutIndexesDictionary);

        var combinedLines = new List<string>();

        foreach (var key in anotherInputDictionary.Keys.Concat(withoutIndexesDictionary.Keys))
        {
            // Get DataItems from both dictionaries (key)
            var item1 = anotherInputDictionary.ContainsKey(key) ? anotherInputDictionary[key] : new DataItem();
            var item2 = withoutIndexesDictionary.ContainsKey(key) ? withoutIndexesDictionary[key] : new DataItem();

            // Combine DataItems
            var line = $"{item1.AdsVariableName},{item2.ModbusAddress ?? "null"},{item2.ModbusPermission ?? "null"},{item1.Type ?? "null"}";
            combinedLines.Add(line);
        }

        // Write combined data to CSV file
        File.WriteAllLines(newfile, combinedLines);
    }

    private static void ReadDataFromFile(string filePath, Dictionary<string, DataItem> dictionary)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 3)
            {
                var item = new DataItem
                {
                    AdsVariableName = parts[0],
                    ModbusAddress = parts[1],
                    ModbusPermission = parts[2],
                    Type = parts.Length >= 4 ? parts[3] : null
                };
                dictionary[item.AdsVariableName] = item;
            }
            else
            {
                // If doesnt have enough elements, add null values for missing fields
                var item = new DataItem
                {
                    AdsVariableName = parts.Length >= 1 ? parts[0] : null,
                    ModbusAddress = parts.Length >= 2 ? parts[1] : null,
                    ModbusPermission = parts.Length >= 3 ? parts[2] : null,
                    Type = null // Add null for Type
                };
                dictionary[item.AdsVariableName] = item;

                Console.WriteLine($"Skipping line: {line}. Not enough elements. Adding null values for missing fields.");
            }
        }
    }
}*/