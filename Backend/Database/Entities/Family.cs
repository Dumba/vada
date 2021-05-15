using System;
using System.Collections.Generic;

namespace Backend.Database.Entities
{
    public class Family
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public ICollection<Member> Members { get; set; } = new Member[0];
    }
}