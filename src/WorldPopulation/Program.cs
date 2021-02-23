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
      Console.WriteLine("1. List the n most populous countries in the list");
      Console.WriteLine("2. List all countries in the list");
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

          int maxToDisplay;
          while (true)
          {
            Console.Write("Enter the number of countries to display: ");
            bool inputIsInt = int.TryParse(Console.ReadLine(), out maxToDisplay);
            if (!inputIsInt || maxToDisplay <= 0)
              Console.WriteLine("You must type in a positive integer.");
            else
              break;
          }
          for (int i = 0; i < countriesList.Count; i++)
          {
            if (i > 0 && (i % maxToDisplay == 0))
            {
              Console.WriteLine("Hit return to continue, or anything else to quit");
              if (Console.ReadLine() != "")
                break;
            }
            Country countryFromList = countriesList[i];
            Console.WriteLine($"{PopulationFormatter.FormatPopulation(countryFromList.Population).PadLeft(15)}: {countryFromList.Name}");
          }

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
