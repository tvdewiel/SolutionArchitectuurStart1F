using CityDAOBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAOBL.Model
{
    public class City
    {
        public City(string name, string description, string country)
        {
            Name = name;
            Description = description;
            Country = country;
        }
        public City(string name, string description, string country, List<Attraction> attractions)
        {
            Name = name;
            Description = description;
            Country = country;
            attractions = attractions;
        }
        public City(int id, string name, string description, string country)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
        }
        public City(int id, string name, string description, string country, List<Attraction> attractions)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
            this.attractions = attractions;
        }

        public int? Id { get; set; }
        private string name = "";
        public string Name
        {
            get { return name; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new CityException("name invalid"); name = value; }
        }
        public string Description { get; set; }
        public string Country { get; set; }
        private List<Attraction> attractions { get; set; } = new();
        public IReadOnlyList<Attraction> Attractions => attractions;
        public bool HasAttraction(Attraction attraction)
        {
            return attractions.Contains(attraction);
        }
        public void AddAttraction(Attraction attraction)
        {
            if (!HasAttraction(attraction)) attractions.Add(attraction);
            else throw new CityException("AddAttraction");
        }
        public void RemoveAttraction(Attraction attraction)
        {
            if (!HasAttraction(attraction)) throw new CityException("RemoveAttraction");
            attractions.Remove(attraction);
        }
        public string ShowCity()
        {
            string x = $"Welcome to {Name}, located in {Country}"
                + $"\n\nThis city can be described as {Description}";
            return x;
        }
        public string ShowCityAttractions()
        {
            string x = $"We have to offer {Attractions.Count} attractions";
            foreach (Attraction attraction in Attractions)
            {
                x += "\n" + attraction.ShowShortDescription();
            }
            return x;
        }
    }
}
