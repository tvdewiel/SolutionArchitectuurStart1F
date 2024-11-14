using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAODL.Model
{
    public class AttractionTO
    {
        public AttractionTO(int? iD, string name, string description, string type, string location, int city_Id, string organizer)
        {
            ID = iD;
            Name = name;
            Description = description;
            Type = type;
            Location = location;
            City_Id = city_Id;
            Organizer = organizer;
        }

        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Organizer { get; set; }
        public int City_Id { get; set; }
    }
}
