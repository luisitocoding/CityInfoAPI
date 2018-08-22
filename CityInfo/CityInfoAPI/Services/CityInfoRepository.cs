using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        //Ahora toca Inyectar el Contexto de datos en el Contructor

        private CityInfoDbContext _context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context;
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointOfInterest.Add(pointOfInterest);
        }

        public bool CityExist(int cityid)
        {
            return _context.Cities.Any(c => c.Id == cityid);
        }

        //Aqui termina la inyeccion

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityid, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                //Include EntityFCore Regresa el objecto con las entidades relacionadas con el 
                return _context.Cities.Include(c => c.PointOfInterest)
                    .Where(c => c.Id == cityid).FirstOrDefault();
            }
            return _context.Cities.Find(cityid);
        }

        public PointOfInterest GetPointOfInterestsForCity(int cityid, int pointofinterest)
        {
            return _context.PointsOfInterest
                .Where(c => c.Id == cityid && c.Id == pointofinterest).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestsForCity(int cityid)
        {
            return _context.PointsOfInterest.Where(p => p.CityId == cityid).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges()>=0);
        }
    }
}
