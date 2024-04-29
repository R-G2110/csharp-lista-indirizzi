using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lista_indirizzi
{
    public class Address
    {
        private string _name;
        private string _surname;
        private string _province;
        private string _zip;

        public string Name
        {
            get { return _name; }
            set
            {
                string trimmedValue = value?.Trim();

                if (string.IsNullOrEmpty(trimmedValue) || trimmedValue.Length < 3)
                {
                    throw new ArgumentException("Name must contain at least 3 characters.");
                }

                _name = trimmedValue;
            }
        }


        public string Surname
        {
            get { return _surname; }
            set
            {
                string trimmedValue = value?.Trim();

                if (string.IsNullOrEmpty(trimmedValue) || trimmedValue.Length < 3)
                {
                    throw new ArgumentException("Surname must contain at least 3 characters.");
                }

                _surname = trimmedValue;
            }
        }


        public string Street { get; set; }
        public string City { get; set; }

        public string Province
        {
            get { return _province; }
            set
            {
                string trimmedValue = value.Trim(); 

                
                if (string.IsNullOrEmpty(trimmedValue))
                {
                    throw new ArgumentException("Province cannot be empty or just whitespace.");
                }
                
                if (trimmedValue.Length > 2)
                {
                    throw new ArgumentException("Province must be a maximum of 2 characters long.");
                }
                
                if (!trimmedValue.All(char.IsLetter))
                {
                    throw new ArgumentException("Province should contain only letters.");
                }

                _province = trimmedValue;
            }
        }


        public string ZIP
        {
            get { return _zip; }
            set
            {
                string trimmedValue = value.Trim();
                if (!trimmedValue.All(char.IsNumber))
                {
                    throw new ArgumentException("ZIP should contain only numbers");
                }
                if (trimmedValue.Length > 5)
                {
                    throw new ArgumentException("ZIP must be a maximum of 5 characters");
                }
                _zip = trimmedValue.PadLeft(5, '0');
            }
        }


        public Address(string name, string surname, string street, string city, string province, string zip)
        {
            Name = name;
            Surname = surname;
            Street = street;
            City = city;
            Province = province;
            ZIP = zip;
        }
    }
}
