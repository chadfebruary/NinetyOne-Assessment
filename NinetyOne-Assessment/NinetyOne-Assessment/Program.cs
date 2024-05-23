using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinetyOne_Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string relativePath = Path.Combine(currentDirectory, @"..\..\Data\TestData.csv");
            string filePath = Path.GetFullPath(relativePath);

            Console.Write("1. Use TestData.csv \n2.Specify file name(Must exist in Data folder)\n");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": List<ScoreData> data = ParseCsv(filePath);
                          DisplayData(data);
                    break;
                case "2": Console.WriteLine("Enter file name: \n");
                          string file = Console.ReadLine();
                          relativePath = Path.Combine(currentDirectory, string.Concat(@"..\..\Data\", file));
                          filePath = Path.GetFullPath(relativePath);
                          data = ParseCsv(filePath);
                          DisplayData(data);
                    break;
                default: Console.WriteLine("Unsupported option");
                    break;
            }

            Console.ReadLine();
        }

        static List<ScoreData> ParseCsv(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string scoreData = File.ReadAllText(filePath);

                    string[] data = scoreData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    List<ScoreData> dataObject = new List<ScoreData>();

                    foreach (var d in data)
                    {
                        string[] splitData = d.Split(',');
                        dataObject.Add(new ScoreData
                        {
                            FirstName = splitData[0],
                            SecondName = splitData[1],
                            Score = splitData[2]
                        });
                    }

                    dataObject = dataObject.OrderByDescending(d => d.Score).ThenBy(d => d.FirstName).ToList();

                    return dataObject;
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("File not found.");
                return null;
            }
        }

        static void DisplayData(List<ScoreData> data)
        {
            if (!(data == null))
            {
                for (int i = 1; i < data.Count; i++)
                {
                    Console.WriteLine(string.Concat(data[i].FirstName, " ", data[i].SecondName));
                    Console.WriteLine(data[i].Score);
                }
            }
            else
            {
                Console.WriteLine("No data to display.");
            }
        }

        public class ScoreData
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string Score { get; set; }
        }
    }
}
