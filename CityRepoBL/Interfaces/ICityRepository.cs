using CityRepoBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityRepoBL.Interfaces
{
    public interface ICityRepository
    {
        void AddAttraction(int id, Attraction attraction);
        void AddCity(City city);
        City GetCity(int id);
    }
}
