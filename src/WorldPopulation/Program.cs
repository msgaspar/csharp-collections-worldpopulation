using System;
using System.Collections.Generic;

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
      Console.WriteLine("2. List all contries in the list");
      Console.WriteLine("3. Find country population by country code");

      string input = Console.ReadLine();
      Console.WriteLine("");

      switch (input)
      {
        case "1":
          Country[] countriesArray = reader.ReadFirstNCountries();
          PopulationFormatter.PrintCountries(countriesArray);
          break;

        case "2":
          List<Country> countriesList = reader.ReadAllCountriesToList();

          // This is an example code that inserts and then immediately removes the country Lilliput, for studying purposes.
          Country lilliput = new Country("Lilliput", "LIL", "Somewhere", 2_000_000);
          int lilliputIndex = countriesList.FindIndex(x => x.Population < 2_000_000);
          countriesList.Insert(lilliputIndex, lilliput);
          countriesList.RemoveAt(lilliputIndex);

          PopulationFormatter.PrintCountries(countriesList);

          break;

        case "3":
          Dictionary<string, Country> countriesDict = reader.ReadAllCountriesToDictionary();

          Console.WriteLine("Which country code do you want to look up? ");
          string userInput = Console.ReadLine();

          bool gotCountry = countriesDict.TryGetValue(userInput.ToUpper(), out Country country);
          if (!gotCountry)
            Console.WriteLine($"Sorry, couldn't find country with code {userInput}");
          else
            Console.WriteLine($"{country.Name} has population {PopulationFormatter.FormatPopulation(country.Population)}");

          break;

        default:
          break;
      }
    }
  }
}
