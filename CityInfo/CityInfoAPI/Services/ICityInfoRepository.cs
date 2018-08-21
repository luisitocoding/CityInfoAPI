using CityInfoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Services
{
    public interface ICityInfoRepository
    {
        //Repositorio que implementara CityInfoRepository
        IEnumerable<City> GetCities();
        City GetCity(int cityid, bool includePointOfInterest);
        bool CityExist(int cityid);
        IEnumerable<PointOfInterest> GetPointsOfInterestsForCity(int cityid);
        PointOfInterest GetPointOfInterestsForCity(int cityid, int pointofinterest);
    }
}
