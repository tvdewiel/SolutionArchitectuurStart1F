using CityRepoBL.Exceptions;
using CityRepoBL.Interfaces;
using CityRepoBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityRepoBL.Managers
{
    public class CityManager
    {
        private ICityRepository cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public void AddCity(City city)
        {
            try
            {
                cityRepository.AddCity(city);
            }
            catch(Exception e) { throw new CityException("AddCity"); }
        }
        public City GetCity(int id)
        {
            try
            {
                return cityRepository.GetCity(id);
            }
            catch (Exception e) { throw new CityException("GetCity"); }
        }
        public void AddAttractionToCity(City city, Attraction attraction) 
        {
            try
            {
                city.AddAttraction(attraction);
                cityRepository.AddAttraction((int)city.Id,attraction);
            }
            catch (Exception e) { throw new CityException("AddCity"); }
        }
    }
}
