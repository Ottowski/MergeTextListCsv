/*using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Globalization;
using static Program;

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
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            string menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                    Console.WriteLine("Creating a new list ...");
                    GenerateNewList();
                    Console.WriteLine("List successfully created!");
                    Console.WriteLine("Press Enter to return to the main menu...");
                    Console.ReadLine();
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
        var projectFolderPath = ""; // Provide folder path destination where CSV files are

        // Find paths to input all CSV files
        var oldListFile1 = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        var oldListFile2 = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        var newListFile = Path.Combine(projectFolderPath, "trying-to-be-perfect-combined-List.csv");

        // Read Data from both files
        List<DataItem> dataList1 = ReadDataFromCsv(oldListFile1);
        List<DataItem> dataList2 = ReadDataFromCsv(oldListFile2);

        // Combine both lists with unique variable name
        Dictionary<string, DataItem> combinedDict = new Dictionary<string, DataItem>();

        // Merge data from the first list
        foreach (var dataItem1 in dataList1)
        {
            if (!combinedDict.ContainsKey(dataItem1.AdsVariableName))
            {
                combinedDict[dataItem1.AdsVariableName] = dataItem1;
            }
            else
            {
                var existingItem = combinedDict[dataItem1.AdsVariableName];
                MergeDataItems(existingItem, dataItem1);
            }
        }

        // Merge data from the second list
        foreach (var dataItem2 in dataList2)
        {
            if (!combinedDict.ContainsKey(dataItem2.AdsVariableName))
            {
                combinedDict[dataItem2.AdsVariableName] = dataItem2;
            }
            else
            {
                var existingItem = combinedDict[dataItem2.AdsVariableName];
                MergeDataItems(existingItem, dataItem2);
            }
        }

        // Convert dictionary values to list
        List<DataItem> combinedList = combinedDict.Values.ToList();

        // Write new combined list to a new file
        WriteDataToCsv(newListFile, combinedList);
    }

    private static void MergeDataItems(DataItem existingItem, DataItem newItem)
    {
        existingItem.ModbusAddress ??= newItem.ModbusAddress;
        existingItem.ModbusPermission ??= newItem.ModbusPermission;
        existingItem.Type ??= newItem.Type;
    }





    private static List<DataItem> ReadDataFromCsv(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Read the header record
            csv.Read();
            csv.ReadHeader();

            // Create a list to store the data
            var dataList = new List<DataItem>();

            // Loop through each record and map it to a DataItem object
            while (csv.Read())
            {
                var dataItem = new DataItem
                {
                    AdsVariableName = csv.GetField("ads variable name"),
                    ModbusAddress = csv.TryGetField("modbus address", out string modbusAddress) ? modbusAddress : null,
                    ModbusPermission = csv.TryGetField("modbus permission", out string modbusPermission) ? modbusPermission : null,
                    Type = csv.TryGetField("type", out string type) ? type : null
                };

                dataList.Add(dataItem);
            }

            return dataList;
        }
    }

    private static void WriteDataToCsv(string filePath, List<DataItem> data)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(data);
        }
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