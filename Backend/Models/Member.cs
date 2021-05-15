using System;
using static Backend.Database.Entities.Member;

namespace Backend.Models
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public Guid FamilyId { get; set; }
    }
}