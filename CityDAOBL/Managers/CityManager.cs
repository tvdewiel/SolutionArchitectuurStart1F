using CityDAOBL.Exceptions;
using CityDAOBL.Model;
using CityDAODL.DAOs;
using CityDAODL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAOBL.Managers
{
    public class CityManager
    {
        private CityDAO cityDAO;
        private AttractionDAO attractionDAO;

        public CityManager(CityDAO cityDAO, AttractionDAO attractionDAO)
        {
            this.cityDAO = cityDAO;
            this.attractionDAO = attractionDAO;
            cityMapper = new CityMapper(cityDAO, attractionDAO);
        }
        private CityMapper cityMapper;

        public void AddCity(City city)
        {
            try
            {
                city.Id = cityDAO.Save(cityMapper.MapCityFromBL(city)).Id;
                foreach (Attraction attraction in city.Attractions)
                {
                    attraction.Id = attractionDAO.Save(cityMapper.MapAttractionFromBL(attraction, city)).ID;
                }
            }
            catch (Exception e)
            {
                throw new CityException("AddCity");
            }
        }
        public void AddAttractionToCity(Attraction attraction, City city)
        {
            try
            {
                city.AddAttraction(attraction);
                AttractionTO aTO = cityMapper.MapAttractionFromBL(attraction, city);
                attractionDAO.Save(aTO);
                attraction.Id = aTO.ID;
            }
            catch (Exception e)
            {
                throw new CityException("AddAttractionToCity");
            }
        }
        public City GetCity(int id)
        {
            try
            {
                return cityMapper.MapCityFromDL(cityDAO.GetById(id));
            }
            catch (Exception e) { throw new CityException("GetCity"); }
        }
        public void DeleteCity(int id)
        {
            try
            {
                if (attractionDAO.CityHasAttractions(id))
                {
                    throw new CityException("City has attractions - no delete");
                }
                cityDAO.Delete(id);
            }
            catch (Exception ex) { throw new CityException("DeleteCity", ex); }
        }
        public void RemoveAttractionFromCity(City city, Attraction attraction)
        {
            try
            {
                city.RemoveAttraction(attraction);
                attractionDAO.Delete((int)attraction.Id);
            }
            catch (Exception e)
            {
                throw new CityException("RemoveAttractionFromCity");
            }
        }
    }
}
