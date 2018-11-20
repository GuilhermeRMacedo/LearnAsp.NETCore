using System.Collections.Generic;
//using System.Linq;
using hello_world_web.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace hello_world_web
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
        //se tiver alguma coisa no banco, retorne
        if(context.Cities.Any())
        {
            return;
        }   

        //dados para serem inseridos previamente
        var cities = new List<City>(){
            new City(){
                Name = "NYC",
                Description = "Big Park",
                PointsOfInterest = new List<PointOfInterest>(){
                    new PointOfInterest(){
                        Name = "Central Park",
                        Description = "The most visited urban park in the united states"
                    },
                    new PointOfInterest(){
                        Name = "Empire State Building",
                        Description = "A 102-story skyscraper located in Midtown Manhattan"
                    }
                } 
            },
            new City(){
                Name = "Paris",
                Description = "The one with that big tower",
                PointsOfInterest = new List<PointOfInterest>(){
                    new PointOfInterest(){
                        Name = "Eiffel Tower",
                        Description = "The tower"
                    },
                    new PointOfInterest(){
                        Name = "The Louvre",
                        Description = "The world's' largest museum"
                    }
                } 
            },
        };

        //adicionar ao contexto usando o metodo AddRange()
        context.Cities.AddRange(cities);
        context.SaveChanges();

        }
    }
}