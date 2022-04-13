using System;

namespace OnlineShopWebApp.Models
{
    public class WorkLocation : IEquatable<WorkLocation>
    {
        public string City { get; set; }
        public WorkLocation(string city)
        {
            City = city;
        }
        public bool Equals(WorkLocation other)
        {
            return City == other.City;
        }
        public override string ToString()
        {
            return City;
        }
    }
}