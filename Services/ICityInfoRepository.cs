using System.Collections.Generic;
using hello_world_web.Entities;

namespace hello_world_web
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfIterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        void AddPointOfIterestForCity(int cityId, PointOfInterest pointOfInterest);

        bool Save();
    }
}