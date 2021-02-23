using System;

namespace WorldPopulation
{
  class Program
  {
    static void Main(string[] args)
    {
      string filePath = @"../../population-data.csv";
      CsvReader reader = new CsvReader(filePath);

      Console.WriteLine("Welcome! Please choose one of the options, or type anything else to quit.");
      Console.WriteLine("1. List the n most populous contries in the list");

      string input = Console.ReadLine();
      Console.WriteLine("");

      switch (input)
      {
        case "1":
          Country[] countries = reader.ReadFirstNCountries();
          foreach (Country country in countries)
          {
            Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
          }
          break;

        default:
          break;
      }


    }
  }
}
