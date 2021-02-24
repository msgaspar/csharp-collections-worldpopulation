using System;
using System.Collections.Generic;

namespace WorldPopulation
{
  class Program
  {
    static void Main(string[] args)
    {
      string filePath = @"population-data.csv";

      Console.WriteLine("Welcome! Please choose one of the options, or type anything else to quit.");
      Console.WriteLine("1. List the n most populous countries in the list");
      Console.WriteLine("2. List all countries in the list");
      Console.WriteLine("3. List all countries in reverse order");
      Console.WriteLine("4. Find country population by country code");

      string input = Console.ReadLine();
      Console.WriteLine("");

      switch (input)
      {
        case "1":
          GetFirstNCountries(filePath);
          break;

        case "2":
          GetAllCountries(filePath);
          break;

        case "3":
          GetAllCountriesInReverseOrder(filePath);
          break;

        case "4":
          GetCountryByCode(filePath);
          break;

        default:
          break;
      }
    }

    private static void GetFirstNCountries(string filePath)
    {
      CsvReader reader = new CsvReader(filePath);
      Country[] countries = reader.ReadFirstNCountries();

      foreach (Country country in countries)
      {
        Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }

    private static void GetAllCountries(string filePath)
    {
      CsvReader reader = new CsvReader(filePath);
      List<Country> countries = reader.ReadAllCountriesToList();

      // This is an example code that inserts and then immediately removes the country Lilliput, for studying purposes.
      Country lilliput = new Country("Lilliput", "LIL", "Somewhere", 2_000_000);
      int lilliputIndex = countries.FindIndex(x => x.Population < 2_000_000);
      countries.Insert(lilliputIndex, lilliput);
      countries.RemoveAt(lilliputIndex);
      //

      int maxToDisplay = AskForMaxToDisplay();

      for (int i = 0; i < countries.Count; i++)
      {
        if (i > 0 && (i % maxToDisplay == 0))
        {
          Console.WriteLine("Hit return to continue, or anything else to quit");
          if (Console.ReadLine() != "")
            break;
        }

        Country country = countries[i];
        Console.WriteLine($"{i + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }

    private static void GetAllCountriesInReverseOrder(string filePath)
    {
      CsvReader reader = new CsvReader(filePath);
      List<Country> countries = reader.ReadAllCountriesToList();

      int maxToDisplay = AskForMaxToDisplay();

      for (int i = 0; i < countries.Count; i++)
      {
        if (i > 0 && (i % maxToDisplay == 0))
        {
          Console.WriteLine("Hit return to continue, or anything else to quit");
          if (Console.ReadLine() != "")
            break;
        }

        Country country = countries[i];
        Console.WriteLine($"{i + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
      }
    }

    private static void GetCountryByCode(string filePath)
    {
      CsvReader reader = new CsvReader(filePath);
      Dictionary<string, Country> countries = reader.ReadAllCountriesToDictionary();

      Console.WriteLine("Which country code do you want to look up? ");
      string userInput = Console.ReadLine();

      bool gotCountry = countries.TryGetValue(userInput.ToUpper(), out Country country);
      if (!gotCountry)
        Console.WriteLine($"Sorry, couldn't find country with code {userInput}");
      else
        Console.WriteLine($"{country.Name} has population {PopulationFormatter.FormatPopulation(country.Population)}");
    }

    private static int AskForMaxToDisplay()
    {
      int userInput;
      while (true)
      {
        Console.Write("Enter the number of countries to display: ");
        bool inputIsInt = int.TryParse(Console.ReadLine(), out userInput);
        if (!inputIsInt || userInput <= 0)
          Console.WriteLine("You must type in a positive integer.");
        else
          break;
      }
      return userInput;
    }
  }
}

