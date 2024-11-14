using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAODL.Model
{
    public class CityTO
    {
        public CityTO(int? id, string name, string description, string country)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
    }
}
