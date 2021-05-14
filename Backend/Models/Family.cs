using System;

namespace Backend.Models
{
    public class Family
    {
        public Guid? Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}