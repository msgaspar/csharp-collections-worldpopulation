using System;
using System.Collections.Generic;
using System.IO;

namespace WorldPopulation
{
  class CsvReader
  {
    private string _csvFilePath;

    public CsvReader(string csvFilePath)
    {
      _csvFilePath = csvFilePath;
    }

    public Country[] ReadFirstNCountries()
    {
      int nCountries;
      while (true)
      {
        Console.WriteLine("How many countries would you like to read?");
        string input = Console.ReadLine();
        var isInt = int.TryParse(input, out nCountries);
        if (!isInt)
        {
          Console.WriteLine("Invalid input.");
        }
        else break;
      }

      Country[] countries = new Country[nCountries];

      using (StreamReader sr = new StreamReader(_csvFilePath))
      {
        // read header line
        sr.ReadLine();

        for (int i = 0; i < nCountries; i++)
        {
          string csvLine = sr.ReadLine();
          countries[i] = ReadCountryFromCsvLine(csvLine);
        }
      }

      return countries;
    }

    public List<Country> ReadAllCountriesToList()
    {
      List<Country> countries = new List<Country>();

      using (StreamReader sr = new StreamReader(_csvFilePath))
      {
        sr.ReadLine();

        string csvLine;
        while ((csvLine = sr.ReadLine()) != null)
        {
          countries.Add(ReadCountryFromCsvLine(csvLine));
        }

        return countries;
      }
    }

    public Dictionary<string, Country> ReadAllCountriesToDictionary()
    {
      var countries = new Dictionary<string, Country>();

      using (StreamReader sr = new StreamReader(_csvFilePath))
      {
        sr.ReadLine();

        string csvLine;
        while ((csvLine = sr.ReadLine()) != null)
        {
          Country country = ReadCountryFromCsvLine(csvLine);
          countries.Add(country.Code, country);
        }

        return countries;
      }
    }

    public Country ReadCountryFromCsvLine(string csvLine)
    {
      string[] parts = csvLine.Split(',');
      string name;
      string code;
      string region;
      string popText;

      switch (parts.Length)
      {
        case 4:
          name = parts[0];
          code = parts[1];
          region = parts[2];
          popText = parts[3];
          break;

        case 5:
          name = parts[0] + "," + parts[1];
          name = name.Replace("\"", null).Trim();
          code = parts[2];
          region = parts[3];
          popText = parts[4];
          break;

        default:
          throw new Exception($"Can't parse country from csvLine: {csvLine}");
      }

      // TryParse leaves population=0 if it can't parse
      int.TryParse(popText, out int population);

      return new Country(name, code, region, population);
    }
  }
}