using System;
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

    public Country ReadCountryFromCsvLine(string csvLine)
    {
      string[] parts = csvLine.Split(new char[] { ',' });

      string name = parts[0];
      string code = parts[1];
      string region = parts[2];
      int population = int.Parse(parts[3]);

      return new Country(name, code, region, population);
    }
  }
}