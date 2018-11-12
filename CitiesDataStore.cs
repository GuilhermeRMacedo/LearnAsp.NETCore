using System.Collections.Generic;

namespace hello_world_web.model
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
         Cities = new List<CityDto>()
         {
             new CityDto(){
                 Id=1, 
                 Name="New York", 
                 Description="The one with that big park",
                 PointsOfInterest = new List<PointsOfInterestDto>()
                 {
                     new PointsOfInterestDto()
                     {
                        Id=1,
                        Name="Central Park",
                        Description="The most visited urban park in the united states"
                     },
                     new PointsOfInterestDto()
                     {
                        Id=2,
                        Name="Empire State Building",
                        Description="A 102-story skyscraper located in Midtown Manhattan"
                     }
                 }
             },
             new CityDto(){
                 Id=2, 
                 Name="Antwerp", 
                 Description="The one with the cathedral that was never really finished",
                 PointsOfInterest = new List<PointsOfInterestDto>(){
                     new PointsOfInterestDto()
                     {
                        Id=1,
                        Name="Cathedral of Our Lady",
                        Description="A Ghotic style cathedral"
                     },
                     new PointsOfInterestDto()
                     {
                        Id=2,
                        Name="Antwerp Central Station",
                        Description="The finnest example of railway archtecture in Bulgium"
                     }
                 }
             },
             new CityDto(){
                 Id=3, 
                 Name="Paris", 
                 Description="The one with that big tower",
                 PointsOfInterest = new List<PointsOfInterestDto>(){
                     new PointsOfInterestDto()
                     {
                        Id=1,
                        Name="Eiffel Tower",
                        Description="The tower"
                     },
                     new PointsOfInterestDto()
                     {
                        Id=2,
                        Name="The Louvre",
                        Description="The world's' largest museum"
                     }
                 }
             }
         }; 
         
        }
    }
}