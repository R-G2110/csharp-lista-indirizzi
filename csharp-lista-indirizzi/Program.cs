using System;
using System.Collections.Generic;


namespace csharp_lista_indirizzi
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "C:\\Users\\39327\\source\\repos\\csharp-lista-indirizzi\\csharp-lista-indirizzi\\addresses.csv";
            List<Address> addressList = ReadAddressesFromFile(filePath);
            PrintAddresses(addressList);

            string outputFilePath = "C:\\Users\\39327\\source\\repos\\csharp-lista-indirizzi\\csharp-lista-indirizzi\\output_addresses.csv";
            SaveAddressesToFile(addressList, outputFilePath);
        }

        private static List<Address> ReadAddressesFromFile(string filePath)
        {
            List<Address> addressList = new List<Address>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    reader.ReadLine(); // Ignore the header

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (!IsValidRow(values, line))
                        {
                            continue;
                        }

                        try
                        {
                            Address address = new Address(values[0], values[1], values[2], values[3], values[4], values[5]);
                            addressList.Add(address);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred on creation: {ex.Message} in this row --> {line}");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Make sure the file path is correct.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }

            return addressList;
        }

        private static bool IsValidRow(string[] values, string line)
        {
            // Check the number of fields
            if (values.Length < 6)
            {
                Console.WriteLine($"\nAttention!! The CSV file does not have enough values in this row --> {line}");
                return false;
            }
            else if (values.Length > 6)
            {
                Console.WriteLine($"\nAttention!! The CSV file has too many values in this row --> {line}");
                return false;
            }

            // Check for empty fields
            List<string> emptyFields = CheckEmptyFields(values);
            if (emptyFields.Any())
            {
                Console.WriteLine($"\nAttention!! The field {string.Join(", ", emptyFields)} is/are empty in this row --> {line}");
                return false;
            }

            return true;
        }

        private static List<string> CheckEmptyFields(string[] values)
        {
            List<string> emptyFields = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(values[i]))
                {
                    emptyFields.Add($"\"{typeof(Address).GetProperties()[i].Name}\"");
                }
            }
            return emptyFields;
        }

        private static void PrintAddresses(List<Address> addresses)
        {
            foreach (var address in addresses)
            {
                Console.WriteLine($"\n* Name: .......... {address.Name} " +
                                  $"\n  Surname: ....... {address.Surname}" +
                                  $"\n  Street: ........ {address.Street}" +
                                  $"\n  City: .......... {address.City}" +
                                  $"\n  Province: ...... {address.Province}" +
                                  $"\n  ZIP: ........... {address.ZIP}");
            }
        }

        private static void SaveAddressesToFile(List<Address> addresses, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Scrivi la riga dell'intestazione con i nomi delle colonne
                    writer.WriteLine("Name,Surname,Street,City,Province,ZIP");

                    // Scrivi ciascun indirizzo nel file
                    foreach (var address in addresses)
                    {
                        writer.WriteLine($"{address.Name},{address.Surname},{address.Street},{address.City},{address.Province},{address.ZIP}");
                    }
                }

                Console.WriteLine($"\nAddresses saved successfully to file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving addresses to file: {ex.Message}");
            }
        }
    }
}


