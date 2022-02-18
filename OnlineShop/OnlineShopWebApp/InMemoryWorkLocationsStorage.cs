using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public class InMemoryWorkLocationsStorage : IWorkLocationsStorage
    {
        private List<WorkLocation> workLocations = new List<WorkLocation>
        {
            new WorkLocation("Самара"),
            new WorkLocation("Кинель"),
            new WorkLocation("Новокуйбышевск"),
            new WorkLocation("Тольятти")
        };
        public List<WorkLocation> GetAllWorkLocations()
        {
            return workLocations;
        }
    }
}