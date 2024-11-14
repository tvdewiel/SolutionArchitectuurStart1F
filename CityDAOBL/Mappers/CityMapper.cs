using CityDAOBL.Model;
using CityDAODL.DAOs;
using CityDAODL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAOBL.Mappers
{
    public class CityMapper
    {
        private AttractionDAO attractionDAO;

        public CityMapper(AttractionDAO attractionDAO)
        {
            this.attractionDAO = attractionDAO;
        }
        public Attraction MapAttractionFromDL(AttractionTO attractionDM)
        {
            return new Attraction(attractionDM.ID, attractionDM.Name, attractionDM.Description, attractionDM.Type, attractionDM.Location, attractionDM.Organizer);
        }
        public City MapCityFromDL(CityTO cityDM)
        {
            List<Attraction> attractions = new List<Attraction>();
            foreach (AttractionTO attractionDM in attractionDAO.GetByCity((int)cityDM.Id))
            {
                attractions.Add(MapAttractionFromDL(attractionDM));
            }
            City city = new City((int)cityDM.Id, cityDM.Name, cityDM.Description, cityDM.Country, attractions);
            return city;
        }
        public AttractionTO MapAttractionFromBL(Attraction attraction, City city)
        {
            return new AttractionTO(attraction.Id, attraction.Name, attraction.Description, attraction.Type, attraction.Location, (int)city.Id, attraction.Organizer);
        }
        public CityTO MapCityFromBL(City city)
        {
            CityTO cityDM = new CityTO(city.Id, city.Name, city.Description, city.Country);
            return cityDM;
        }
    }
}
