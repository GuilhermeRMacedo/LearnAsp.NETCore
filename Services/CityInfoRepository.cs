using System.Collections.Generic;
using System.Linq;
using hello_world_web.Entities;
using Microsoft.EntityFrameworkCore;

namespace hello_world_web.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public bool CityExists(int cityId)
        {
            //in mysql i had to transform both in strings, 
            //if not will throw one exception(comparing int16 with string)
            return _context.Cities.Any(c => c.Id.ToString() == cityId.ToString());
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities
            .OrderBy(c => c.Name)
            .ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if(includePointsOfInterest)
            {
                return _context.Cities
                .Include(c => c.PointsOfInterest)
                .Where(c => c.Id == cityId)
                .FirstOrDefault();
            }   
            return _context.Cities
            .Where(c => c.Id == cityId)
            .FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
            .FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfIterestForCity(int cityId)
        {
            return _context.PointsOfInterest
            .Where(p => p.CityId == cityId)
            .ToList();
        }
    }
}