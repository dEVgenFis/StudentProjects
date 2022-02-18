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
            if (other == null)
                return false;
            if (this.City == other.City)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return City;
        }
    }
}