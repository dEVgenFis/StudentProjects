using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IWorkLocationsStorage
    {
        List<WorkLocation> GetAllWorkLocations();
    }
}