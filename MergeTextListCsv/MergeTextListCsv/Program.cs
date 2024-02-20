using System;
using System.IO;

class Program
{
    static List<string> combinedLines = new List<string>(); // Declared at the class level

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
        string projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;

        string oldListFile1 = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        string oldListFile2 = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        string newListFile = Path.Combine(projectFolderPath, "new-example-of-combined-List.csv");

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
        string projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        var anotherInputFile = Path.Combine(projectFolderPath, "example-input-list-another-input.csv");
        var withoutIndexesFile = Path.Combine(projectFolderPath, "example-input-list-without-indexes.csv");
        var newfile = Path.Combine(projectFolderPath, "indexList.csv");

        var anotherInputDictionary = new Dictionary<string, DataItem>();
        var withoutIndexesDictionary = new Dictionary<string, DataItem>();

        // Read data from the 'example-input-list-another-input.csv' file
        ReadDataFromFile(anotherInputFile, anotherInputDictionary);

        // Read data from the 'example-input-list-without-indexes.csv' file
        ReadDataFromFile(withoutIndexesFile, withoutIndexesDictionary);

        // Combine data based on common keys
        var combinedLines = new List<string>();
        combinedLines.Add("ads variable name,modbus address,modbus permission,type");

        foreach (var key in anotherInputDictionary.Keys)
        {
            if (withoutIndexesDictionary.TryGetValue(key, out var item))
            {
                combinedLines.Add($"{item.AdsVariableName},{item.ModbusAddress},{item.ModbusPermission},{item.Type}");
            }
            else
            {
                item = anotherInputDictionary[key];
                combinedLines.Add($"{item.AdsVariableName},{item.ModbusAddress},{item.ModbusPermission},{item.Type}");
            }
        }

        // Write combined data to the output CSV file
        File.WriteAllLines(newfile, combinedLines);
    }

    private static void ReadDataFromFile(string filePath, Dictionary<string, DataItem> dictionary)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split('\t');
            if (parts.Length >= 3)
            {
                var item = new DataItem
                {
                    AdsVariableName = parts[0],
                    ModbusAddress = parts[1],
                    ModbusPermission = parts[2],
                    Type = parts.Length >= 4 ? parts[3] : ""
                };
                dictionary[item.AdsVariableName] = item;
            }
            else
            {
                Console.WriteLine($"Skipping line: {line}. Not enough elements.");
            }
        }
    }

}


/*private static void IndexList()
{
    string[] anotherInputLines = File.ReadAllLines("example-input-list-another-input.csv");
    Dictionary<string, DataItem> anotherInputDictionary = new Dictionary<string, DataItem>();
    foreach (string line in anotherInputLines)
    {
        string[] parts = line.Split('\t');
        if (parts.Length >= 3) 
        {
            var item = new DataItem
            {
                AdsVariableName = parts[0],
                ModbusAddress = parts[1],
                ModbusPermission = parts[2],
                Type = "" 
            };
            anotherInputDictionary[item.AdsVariableName] = item;
        }
        else
        {
            Console.WriteLine($"Skipping line: {line}. Not enough elements.");
        }
    }


    string[] withoutIndexesLines = File.ReadAllLines("example-input-list-without-indexes.csv");
    Dictionary<string, DataItem> withoutIndexesDictionary = new Dictionary<string, DataItem>();
    foreach (string line in withoutIndexesLines)
    {
        string[] parts = line.Split('\t');
        if (parts.Length >= 3) // Ensure that there are at least three parts after splitting
        {
            var item = new DataItem
            {
                AdsVariableName = parts[0],
                ModbusAddress = parts[1],
                ModbusPermission = parts[2],
                Type = parts.Length >= 4 ? parts[3] : ""
            };
            withoutIndexesDictionary[item.AdsVariableName] = item;
        }
        else
        {
            Console.WriteLine($"Skipping line: {line}. Not enough elements.");
        }
    }

    combinedLines.Clear(); // Clear the list before adding new lines

    // Combine data based on common keys
    combinedLines.Add("ads variable name\tmodbus address\tmodbus permission\ttype");
    foreach (var key in anotherInputDictionary.Keys)
    {
        if (withoutIndexesDictionary.TryGetValue(key, out var item))
        {
            combinedLines.Add($"{item.AdsVariableName}\\s+{item.ModbusAddress}\\s+{item.ModbusPermission}\\s+{item.Type}");
        }
        else
        {
            item = anotherInputDictionary[key];
            combinedLines.Add($"{item.AdsVariableName}\\s+{item.ModbusAddress}\\s+{item.ModbusPermission}\\s+{item.Type}");
        }
    }

    File.WriteAllLines("indexList.csv", combinedLines);
}*/


