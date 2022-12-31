using Datas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Interfaces
{
   public  interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        City GetSingleCitiesAsync(int id);

        void AddCity(City city);
        void DeleteCity(int CityId);

        Task<City> FindCity(int id);

        // Another Type Update
        //void UpdateCity(City city);

    }
}
